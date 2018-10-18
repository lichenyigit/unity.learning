using System.Collections;
using System.Reflection;
using UnityEngine;

public class DynamicCSController : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		string url = "http://ftp.lichenyi.cn/model/dynamicCS.assetbundle";
		WWW www = new WWW(url);
		StartCoroutine(loadModel(www));
		

	}
	
	private IEnumerator loadModel(WWW www)
	{
		yield return www;
		
		GameObject dynamicCS = Instantiate(www.assetBundle.mainAsset) as GameObject;
		www.Dispose();
		Assembly assembly = Assembly.Load(((TextAsset) www.assetBundle.mainAsset).bytes);
		var types = assembly.GetTypes();
		foreach (var type in types)
		{
			Debug.Log(type.Name);
		}
		/*Debug.Log(Instantiate(www.assetBundle.mainAsset));
		GameObject dynamicCS = Instantiate(www.assetBundle.mainAsset) as GameObject;
		www.assetBundle.Unload(false);
		/*dynamicCS.transform.parent = gameObject.transform;#1#

		byte[] fs = www.bytes;
		var b = new byte[fs.Length];
		var assembly = System.Reflection.Assembly.Load(b);
		var type = assembly.GetType("dynamicCS");
		gameObject.AddComponent(type);*/
	}
}
