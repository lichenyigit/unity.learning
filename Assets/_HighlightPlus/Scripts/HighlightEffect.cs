﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HighlightPlus {

	public delegate void OnObjectHighlightStartEvent (GameObject obj, ref bool cancelHighlight);
	public delegate void OnObjectHighlightEndEvent (GameObject obj);


	[ExecuteInEditMode]
	public class HighlightEffect : MonoBehaviour {

		public enum SeeThroughMode {
			WhenHighlighted = 0,
			Always = 1,
			Never = 2
		}

		[Serializable]
		public struct GlowPassData {
			[SerializeField]
			public float offset;
			[SerializeField]
			public float alpha;
			[SerializeField]
			public Color color;
		}

		[Serializable]
		struct ModelMaterials {
			[SerializeField]
			public Transform transform;
			[SerializeField]
			public Mesh mesh;
			[SerializeField]
			public SkinnedMeshRenderer skinnedMeshRenderer;
			[SerializeField]
			public Material material;
			[SerializeField]
			public Material fxMatSeeThrough, fxMatGlow, fxMatOutline, fxMatOverlay;
		}

		public bool previewInEditor;
		public Transform target;

		public bool highlighted;

		[Range (0, 1)]
		public float overlay = 0.5f;
		public Color overlayColor = Color.yellow;
		public float overlayAnimationSpeed = 1f;

		public Color outlineColor = Color.black;
		public float outlineWidth = 0.45f;

		[Range (0, 5)]
		public float glow = 1f;
		public float glowWidth = 0.4f;
		public bool glowDithering = true;
		public float glowMagicNumber1 = 0.75f;
		public float glowMagicNumber2 = 0.5f;
		public float glowAnimationSpeed = 1f;
		public GlowPassData[] glowPasses;

		public event OnObjectHighlightStartEvent OnObjectHighlightStart;
		public event OnObjectHighlightEndEvent OnObjectHighlightEnd;

		public SeeThroughMode seeThrough;
		[Range (0, 5f)]
		public float seeThroughIntensity = 0.8f;
		[Range (0, 1)]
		public float seeThroughTintAlpha = 0.5f;
		public Color seeThroughTintColor = Color.red;


		[SerializeField, HideInInspector]
		ModelMaterials[] rms;
		[SerializeField, HideInInspector]
		int rmsCount = 0;

		public static Material fxMatMask, fxMatSeeThrough, fxMatGlow, fxMatOutline, fxMatOverlay;

		void OnEnable () {
			if (target == null)
				target = transform;
			if (glowPasses == null || glowPasses.Length == 0) {
				glowPasses = new GlowPassData[4];
				glowPasses [0] = new GlowPassData () { offset = 4, alpha = 0.1f, color = new Color (0.64f, 1f, 0f, 1f) };
				glowPasses [1] = new GlowPassData () { offset = 3, alpha = 0.2f, color = new Color (0.64f, 1f, 0f, 1f) };
				glowPasses [2] = new GlowPassData () { offset = 2, alpha = 0.3f, color = new Color (0.64f, 1f, 0f, 1f) };
				glowPasses [3] = new GlowPassData () { offset = 1, alpha = 0.4f, color = new Color (0.64f, 1f, 0f, 1f) };
			}
			InitMaterial (ref fxMatMask, "HighlightPlus/Geometry/Mask");
			InitMaterial (ref fxMatSeeThrough, "HighlightPlus/Geometry/SeeThrough");
			InitMaterial (ref fxMatGlow, "HighlightPlus/Geometry/Glow");
			InitMaterial (ref fxMatOutline, "HighlightPlus/Geometry/Outline");
			InitMaterial (ref fxMatOverlay, "HighlightPlus/Geometry/Overlay");
			SetupMaterial ();
		}

		private void OnDisable () {
			UpdateMaterialProperties ();
		}

		public void Refresh () {
			if (!enabled) {
				enabled = true;
			} else {
				SetupMaterial ();
			}
		}

		void LateUpdate () {

#if UNITY_EDITOR
			if (!previewInEditor && !Application.isPlaying)
				return;
#endif
			bool seeThroughReal = this.seeThrough == SeeThroughMode.Always || (this.seeThrough == SeeThroughMode.WhenHighlighted && highlighted);
			if (!highlighted && !seeThroughReal) {
				return;
			}

			float glowReal = this.highlighted ? this.glow : 0;
			MaterialPropertyBlock glowProperties = new MaterialPropertyBlock ();
			int layer = gameObject.layer;
			for (int k = 0; k < rms.Length; k++) {
				Transform t = rms [k].transform;
				if (t == null)
					continue;
				Matrix4x4 matrix = Matrix4x4.TRS (t.position, t.rotation, t.lossyScale);
				Mesh mesh = rms [k].mesh;
				if (rms [k].skinnedMeshRenderer != null) {
					rms [k].skinnedMeshRenderer.BakeMesh (mesh);
				}
				if (mesh == null)
					continue;
				for (int l = 0; l < mesh.subMeshCount; l++) {
					Graphics.DrawMesh (mesh, matrix, fxMatMask, layer, null, l);
					if (seeThroughReal) {
						Graphics.DrawMesh (mesh, matrix, rms [k].fxMatSeeThrough, layer);
					}
					if (highlighted) {
						for (int j = 0; j < glowPasses.Length; j++) {
							glowProperties.SetColor ("_GlowColor", glowPasses [j].color);
							glowProperties.SetVector ("_Glow", new Vector4 (glowReal * glowPasses [j].alpha, glowPasses [j].offset * glowWidth / 100f, glowMagicNumber1, glowMagicNumber2));
							Graphics.DrawMesh (mesh, matrix, rms [k].fxMatGlow, layer, null, l, glowProperties);
						}
						Graphics.DrawMesh (mesh, matrix, rms [k].fxMatOutline, layer, null, l);
						Graphics.DrawMesh (mesh, matrix, rms [k].fxMatOverlay, layer, null, l);
					}
				}
			}
		}

		void InitMaterial (ref Material material, string shaderName) {
			if (material == null) {
				Shader shaderFX = Shader.Find (shaderName);
				if (shaderFX == null) {
					Debug.LogError ("Shader " + shaderName + " not found.");
					enabled = false;
					return;
				}
				material = new Material (shaderFX);
			}
		}

		public void SetTarget (Transform transform) {
			if (transform == target || transform == null)
				return;

			if (highlighted) {
				SetHighlighted (false);
			}

			target = transform;
			SetupMaterial ();
		}

		public void SetHighlighted (bool state) {
			bool cancelHighlight = false;
			if (state) {
				if (OnObjectHighlightStart != null) {
					OnObjectHighlightStart (gameObject, ref cancelHighlight);
					if (cancelHighlight) {
						return;
					}
				}
				SendMessage ("HighlightStart", null, SendMessageOptions.DontRequireReceiver);
			} else {
				if (OnObjectHighlightEnd != null) {
					OnObjectHighlightEnd (gameObject);
				}
				SendMessage ("HighlightEnd", null, SendMessageOptions.DontRequireReceiver);
			}
			highlighted = state;
			Refresh ();
		}

		void SetupMaterial () {

			Renderer[] rr = target.GetComponentsInChildren<Renderer> ();
			if (rms == null || rms.Length < rr.Length) {
				rms = new ModelMaterials[rr.Length];
			}
			rmsCount = 0;
			for (int k = 0; k < rr.Length; k++) {
				rms [rmsCount] = new ModelMaterials ();
				Renderer renderer = rr [k];
				if (renderer.transform != target && renderer.GetComponent<HighlightEffect> () != null)
					continue; // independent subobject
				if (renderer is SkinnedMeshRenderer) {
					SkinnedMeshRenderer smr = (SkinnedMeshRenderer)renderer;
					rms [rmsCount].mesh = new Mesh ();
					rms [rmsCount].skinnedMeshRenderer = smr;
				} else if (renderer.gameObject.isStatic) {
					MeshCollider mc = renderer.GetComponent<MeshCollider> ();
					if (mc != null) {
						rms [rmsCount].mesh = mc.sharedMesh;
					}
				}
				if (rms [rmsCount].mesh == null) {
					MeshFilter mf = renderer.GetComponent<MeshFilter> ();
					if (mf != null) {
						rms [rmsCount].mesh = mf.sharedMesh;
					}
				}
				rms [rmsCount].transform = renderer.transform;
				rms [rmsCount].material = renderer.sharedMaterial;
				rms [rmsCount].fxMatSeeThrough = Instantiate<Material> (fxMatSeeThrough);
				rms [rmsCount].fxMatGlow = Instantiate<Material> (fxMatGlow);
				rms [rmsCount].fxMatOutline = Instantiate<Material> (fxMatOutline);
				rms [rmsCount].fxMatOverlay = Instantiate<Material> (fxMatOverlay);
				rmsCount++;
			}
			UpdateMaterialProperties ();
		}

		void UpdateMaterialProperties () {
			Color outlineColor = this.outlineColor;
			Color overlayColor = this.overlayColor;
			overlayColor.a = overlay;
			Color seeThroughTintColor = this.seeThroughTintColor;
			seeThroughTintColor.a = this.seeThroughTintAlpha;

			for (int k = 0; k < rmsCount; k++) {
				if (rms [k].mesh != null) {
					// Setup materials
					Material mat = rms [k].material;
					// See-through
					Material fxMat = rms [k].fxMatSeeThrough;
					if (mat.HasProperty ("_MainTex")) {
						fxMat.mainTexture = mat.mainTexture;
						fxMat.mainTextureOffset = mat.mainTextureOffset;
						fxMat.mainTextureScale = mat.mainTextureScale;
					}
					fxMat.SetFloat ("_SeeThrough", seeThroughIntensity);
					fxMat.SetColor ("_SeeThroughTintColor", seeThroughTintColor);

					// Glow
					fxMat = rms [k].fxMatGlow;
					fxMat.SetVector ("_Glow2", new Vector3 (outlineWidth / 100f, glowAnimationSpeed, glowDithering ? 0 : 1));

					// Outline
					fxMat = rms [k].fxMatOutline;
					fxMat.SetColor ("_OutlineColor", outlineColor);
					fxMat.SetFloat ("_OutlineWidth", outlineWidth / 100f);

					// Overlay
					fxMat = rms [k].fxMatOverlay;
					if (mat.HasProperty ("_MainTex")) {
						fxMat.mainTexture = mat.mainTexture;
						fxMat.mainTextureOffset = mat.mainTextureOffset;
						fxMat.mainTextureScale = mat.mainTextureScale;
					}
					if (mat.HasProperty ("_Color")) {
						fxMat.SetColor ("_OverlayBackColor", mat.GetColor ("_Color"));
					}
					fxMat.color = overlayColor;
					fxMat.SetFloat ("_OverlaySpeed", overlayAnimationSpeed);
				}
			}
		}


	}

}