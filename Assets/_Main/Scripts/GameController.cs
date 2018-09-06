using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using Ping = UnityEngine.Ping;

public class GameController : MonoBehaviour
{
    
    //模型网络地址
    private string modelNameFormat = "http://ftp.lichenyi.cn/model/{0}.assetbundle";

    //模型名称
    private string[] modelName = {"001", "002", "003"};
    public Slider loadingBarSlider;
    //private GameObject model; //模型
    private float startTime; //程序开始时间
    private float timeStep = 0.05f;
    private float oneStepValue = 0.01f;

    private void Start()
    {
        /*
        //远程服务器文件是否存在
        bool isExists = remoteFileExists("http://ftp.lichenyi.cn/model/{0}.assetbundle");
        Debug.Log("{0}.assetbundle 存在:" + isExists);
        isExists = remoteFileExists("http://ftp.lichenyi.cn/model/001.assetbundle");
        Debug.Log("001.assetbundle 存在:" + isExists);
        */
        loadingBarSlider.gameObject.SetActive(true);
        string url = String.Format(modelNameFormat, modelName[0]);
        WWW www = new WWW(url);
        StartCoroutine(showProgressAndLoadModel(www));
    }

    private IEnumerator showProgressAndLoadModel(WWW www)
    {
        startTime = Time.time;
        while (!www.isDone || loadingBarSlider.value < 0.8)
        {
            if (Time.time - startTime >= timeStep)
            {
                loadingBarSlider.value += oneStepValue;
                startTime = Time.time;
            }

            yield return new WaitForEndOfFrame();
        }

        if (www.isDone)
        {
            while (loadingBarSlider.value < 1)
            {
                if (Time.time - startTime >= timeStep)
                {
                    loadingBarSlider.value += oneStepValue;
                    startTime = Time.time;
                }

                yield return new WaitForEndOfFrame();
            }

            GameObject model = Instantiate(www.assetBundle.mainAsset) as GameObject;
            displayModel(model); //显示模型
        }
    }

    private void displayModel(GameObject model)
    {
        model.SetActive(true);
        model.transform.localPosition = new Vector3(0, 0, 0);
        model.transform.localScale = new Vector3(1f, 1f, 1f);
        model.transform.parent = transform;
        model.gameObject.AddComponent<ModelRotation>();
    }


    /// <summary>
    /// 判断服务器端文件是否存在
    /// </summary>
    /// <param name="fileUrl"></param>
    /// <returns></returns>
    private bool remoteFileExists(string fileUrl)
    {
        bool result = false; //下载结果

        WebResponse response = null;
        try
        {
            WebRequest req = WebRequest.Create(fileUrl);

            response = req.GetResponse();

            result = response == null ? false : true;
        }
        catch (Exception ex)
        {
            result = false;
        }
        finally
        {
            if (response != null)
            {
                response.Close();
            }
        }

        return result;
    }
}