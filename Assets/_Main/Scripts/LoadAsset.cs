using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Object = System.Object;

public class LoadAsset : MonoBehaviour
{
    public int a = 0;

    public static LoadAsset initializ;
    
    private void Awake()
    {
        if (initializ == null)
        {
            initializ = this;
        }
        else if(initializ != this)
        {
            Destroy(initializ);
        }

    }

    void Start()
    {
    }

    private void Update()
    {
    }

    public void changeScene()
    {
        SceneManager.LoadScene("Game1");
    }
    
    /*private IEnumerator loadAsset()
    {
        Debug.Log("开始加载");
        //string url = "http://ftp.lichenyi.cn/database/ARSeeUSessionConfig.assetbundle";
        string url = "http://ftp.lichenyi.cn/model/001.assetbundle";
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            Debug.Log("加载中");
            yield return new WaitForEndOfFrame();
        }

        if (www.isDone)
        {
            Debug.Log("加载完毕");
            gameObject = Instantiate(www.assetBundle.mainAsset) as GameObject;
        }
    }*/
}