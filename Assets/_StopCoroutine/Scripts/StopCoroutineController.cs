using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities;
using UnityEngine;

public class StopCoroutineController : MonoBehaviour
{
    List<Coroutine> coroutineList = new List<Coroutine>();
    string url = "http://ftp.lichenyi.cn/model/AudiR8.assetbundle";

    void Start()
    {
        WWW www = new WWW(url);
        addCoroutineList(StartCoroutine(test(www)));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("终止");
            if (coroutineList != null && coroutineList.Count > 0)
            {
                foreach (Coroutine coroutine in coroutineList)
                {
                    StopCoroutine(coroutine);
                }
            }

            Debug.Log("终止成功\r\n重新加载");
            WWW www = new WWW(url);
            addCoroutineList(StartCoroutine(test(www)));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            WWW www = new WWW(url);
            addCoroutineList(StartCoroutine(test(www)));
        }
    }

    IEnumerator test(WWW www)
    {
        yield return new WaitForEndOfFrame();
        while (!www.isDone)
        {
            Debug.Log("加载" + www.progress);
            yield return new WaitForSeconds(1);
        }

        GameObject model = Instantiate(www.assetBundle.mainAsset) as GameObject;
        Debug.Log(model.name);
        model.SetActive(true);
    }

    private void addCoroutineList(Coroutine coroutine)
    {
        Debug.Log(coroutine);
        coroutineList.Add(coroutine);
    }

}