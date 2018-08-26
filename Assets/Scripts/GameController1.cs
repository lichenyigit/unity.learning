using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using Ping = UnityEngine.Ping;

public class GameController1 : MonoBehaviour
{
    //模型网络地址
    private string modelNameFormat = "http://ftp.lichenyi.cn/model/{0}.assetbundle";

    //模型名称
    private string[] modelName = {"001", "002"};
    private Dictionary<int, GameObject> modelOfIndexDictionary = new Dictionary<int, GameObject>();

    public Slider loadingBarSlider;

    //模型集合
    private float startTimeOfAnimation; //程序开始时间
    private float timeStep = 0.05f;
    private float oneStepValue = 0.01f;
    private WWW www;
    
    private void Start()
    {
        loadingBarSlider.gameObject.SetActive(true);
        string url = String.Format(modelNameFormat, modelName[0]);
        www = new WWW(url);
        StartCoroutine(showProgressAndLoadModel(www));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("重新加载");
            destoryModel();
            
            //重新加载
            loadingBarSlider.gameObject.SetActive(true);
            string url = String.Format(modelNameFormat, modelName[0]);
            WWW www = new WWW(url);
            StartCoroutine(showProgressAndLoadModel(www));
        }
    }


    /// <summary>
    /// 显示加载动画,同时加载模型
    /// </summary>
    /// <param name="www"></param>
    /// <returns></returns>
    private IEnumerator showProgressAndLoadModel(WWW www)
    {
        showLodingAnimation(); //显示加载动画
        startTimeOfAnimation = Time.time;
        //将动画进度条加载到0.8
        while (!www.isDone || loadingBarSlider.value < 0.8)
        {
            if (Time.time - startTimeOfAnimation >= timeStep && loadingBarSlider.gameObject.active)
            {
                loadingBarSlider.value += oneStepValue;
                startTimeOfAnimation = Time.time;
            }

            yield return new WaitForEndOfFrame();
        }

        if (www.isDone)
        {
            //将动画进度条加载到1
            while (loadingBarSlider.value < 1)
            {
                if (Time.time - startTimeOfAnimation >= timeStep)
                {
                    loadingBarSlider.value += oneStepValue;
                    startTimeOfAnimation = Time.time;
                }

                yield return new WaitForEndOfFrame();
            }
        }
        
        GameObject model = Instantiate(www.assetBundle.mainAsset) as GameObject;
        //模型添加至modelDictionary
        displayModel(model, Vector3.forward); //显示网络模型
    }

    /// <summary>
    /// 显示网络模型
    /// </summary>
    private void displayModel(GameObject model, Vector3 position)
    {
        model.SetActive(true);
        hideLoadingAnimation(); //隐藏加载动画
        model.transform.localPosition = position;
        model.transform.localScale = new Vector3(1f, 1f, 1f);
        model.transform.parent = transform;
        model.gameObject.AddComponent<ModelRotation>();
    }

    /// <summary>
    /// 显示加载动画
    /// </summary>
    private void showLodingAnimation()
    {
        loadingBarSlider.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏加载动画
    /// </summary>
    private void hideLoadingAnimation()
    {
        loadingBarSlider.gameObject.SetActive(false);
        loadingBarSlider.value = 0;
    }

    /// <summary>
    /// 隐藏所有的模型
    /// </summary>
    private void hideAllModel()
    {
//        foreach (GameObject model in modelOfIndexDictionary.Values)
//        {
//            model.SetActive(false);
//        }
        Destroy(transform.GetChild(0).gameObject);
        
    }

    public void destoryModel()
    {
        foreach (var assetBundle in AssetBundle.GetAllLoadedAssetBundles())
        {
          Debug.Log(assetBundle.name);   
//            AssetBundle.Destroy(assetBundle);
        }
        www.Reset();
        hideAllModel(); //隐藏所有的动画
        hideLoadingAnimation(); //隐藏加载动画
    }
}