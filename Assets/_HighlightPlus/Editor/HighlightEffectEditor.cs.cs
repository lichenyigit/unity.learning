using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HighlightPlus {
	[CustomEditor (typeof(HighlightEffect))]
	[CanEditMultipleObjects]
	public class HighlightEffectEditor : Editor {

		SerializedProperty previewInEditor, highlighted;
		SerializedProperty overlay, overlayColor, overlayAnimationSpeed;
		SerializedProperty outlineColor, outlineWidth;
		SerializedProperty glow, glowWidth, glowDithering, glowMagicNumber1, glowMagicNumber2, glowAnimationSpeed, glowPasses;
		SerializedProperty seeThrough, seeThroughIntensity, seeThroughTintAlpha, seeThroughTintColor;

		void OnEnable () {
			previewInEditor = serializedObject.FindProperty ("previewInEditor");
			highlighted = serializedObject.FindProperty ("highlighted");
			overlay = serializedObject.FindProperty ("overlay");
			overlayColor = serializedObject.FindProperty ("overlayColor");
			overlayAnimationSpeed = serializedObject.FindProperty ("overlayAnimationSpeed");
			outlineColor = serializedObject.FindProperty ("outlineColor");
			outlineWidth = serializedObject.FindProperty ("outlineWidth");
			glow = serializedObject.FindProperty ("glow");
			glowWidth = serializedObject.FindProperty ("glowWidth");
			glowAnimationSpeed = serializedObject.FindProperty ("glowAnimationSpeed");
			glowDithering = serializedObject.FindProperty ("glowDithering");
			glowMagicNumber1 = serializedObject.FindProperty ("glowMagicNumber1");
			glowMagicNumber2 = serializedObject.FindProperty ("glowMagicNumber2");
			glowAnimationSpeed = serializedObject.FindProperty ("glowAnimationSpeed");
			glowPasses = serializedObject.FindProperty ("glowPasses");
			seeThrough = serializedObject.FindProperty ("seeThrough");
			seeThroughIntensity = serializedObject.FindProperty ("seeThroughIntensity");
			seeThroughTintAlpha = serializedObject.FindProperty ("seeThroughTintAlpha");
			seeThroughTintColor = serializedObject.FindProperty ("seeThroughTintColor");
		}

		public override void OnInspectorGUI () {
			HighlightEffect thisEffect = (HighlightEffect)target;
			bool isNotManager = thisEffect.GetComponent<HighlightManager> () == null;

			EditorGUILayout.Separator ();
			serializedObject.Update ();
			if (isNotManager) {
				EditorGUILayout.PropertyField (previewInEditor);
			}
			EditorGUILayout.Separator ();
			EditorGUILayout.LabelField("Highlight Options", EditorStyles.boldLabel);
			EditorGUI.BeginChangeCheck ();
			if (isNotManager) {
				EditorGUILayout.PropertyField (highlighted);
			}
			EditorGUILayout.PropertyField (overlay);
			EditorGUILayout.PropertyField (overlayColor);
			EditorGUILayout.PropertyField (overlayAnimationSpeed);
			EditorGUILayout.PropertyField (outlineColor);
			EditorGUILayout.PropertyField (outlineWidth);
			EditorGUILayout.PropertyField (glow);
			EditorGUILayout.PropertyField (glowWidth);
			EditorGUILayout.PropertyField (glowAnimationSpeed);
			EditorGUILayout.PropertyField (glowDithering);
			if (glowDithering.boolValue) {
				EditorGUILayout.PropertyField (glowMagicNumber1);
				EditorGUILayout.PropertyField (glowMagicNumber2);
			}
			EditorGUILayout.PropertyField (glowPasses, true);
			EditorGUILayout.Separator ();
			EditorGUILayout.LabelField("See-Through Options", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField (seeThrough);
			EditorGUILayout.PropertyField (seeThroughIntensity);
			EditorGUILayout.PropertyField (seeThroughTintAlpha);
			EditorGUILayout.PropertyField (seeThroughTintColor);

			if (serializedObject.ApplyModifiedProperties ()) {
				foreach (HighlightEffect effect in targets) {
					effect.Refresh ();
				}
			}
			HighlightEffect _effect = (HighlightEffect)target;
			if (_effect != null && _effect.previewInEditor) {
				EditorUtility.SetDirty (_effect);
			}
		}


		[MenuItem ("GameObject/Highlight Plus/Create Manager")]
		static void CreateManager (MenuCommand menuCommand) {
			HighlightManager manager = FindObjectOfType<HighlightManager> ();
			if (manager == null) {
				GameObject managerGO = new GameObject ("HighlightPlusManager");
				managerGO.name = "HighlightPlusManager";
				manager = managerGO.AddComponent<HighlightManager> ();
			}

			// Register root object for undo.
			Undo.RegisterCreatedObjectUndo (manager, "Create Highlight Plus Manager");
			Selection.activeObject = manager;
		}



	}

}