using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ReloadAssetBundle : MonoBehaviour
{
    private string url = "http://cdn.lichenyi.cn/model/HumanSkull.assetbundle";
    private WWW www;
    private bool sign = true;
    
    void Start()
    {
        www = new WWW(url);
        Debug.Log("开始加载");
        StartCoroutine(load(UnityWebRequestAssetBundle.GetAssetBundle(url)));
    }

    private void Update()
    {
        return;
        if (www.isDone && sign)
        {
            sign = false;
            Debug.Log(1);
            /*GameObject obj = www.assetBundle.mainAsset as GameObject;
            Debug.Log(obj);
            obj.transform.SetParent(gameObject.transform);
            Debug.Log(2);*/
            //Debug.Log(www.assetBundle);
            AssetBundleCreateRequest assetBundleReq = AssetBundle.LoadFromMemoryAsync(www.bytes);
            Debug.Log(2);
            Debug.Log(assetBundleReq.assetBundle);
            /*Object obj = www.assetBundle.mainAsset;
            Debug.Log(obj);*/
        }
        else
        {
            //AssetBundle.LoadFromStreamAsync(www)
        }
    }

    private IEnumerator load(UnityWebRequest uwr)
    {
        Debug.Log(2);
        yield return uwr.SendWebRequest();
        Debug.Log(3);
        Instantiate(DownloadHandlerAssetBundle.GetContent(uwr).mainAsset);
        Debug.Log(4);
    }
}