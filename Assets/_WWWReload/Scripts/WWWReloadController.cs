using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Object = System.Object;

public class WWWReloadController : MonoBehaviour
{
    //模型名称
    private Dictionary<int, GameObject> modelOfIndexDictionary = new Dictionary<int, GameObject>();

    public Slider loadingBarSlider;

    //模型集合
    private float startTimeOfAnimation; //程序开始时间
    private float timeStep = 0.05f;
    private float oneStepValue = 0.03f;
    private Vector3 scale;

    List<Coroutine> coroutineList = new List<Coroutine>();

    //private string url = "https://arseeu.oss-cn-hangzhou.aliyuncs.com/model/AudiR8.assetbundle";
    private string url = "http://cdn.lichenyi.cn/model/HumanSkull.assetbundle";

    private void addCoroutineList(Coroutine coroutine)
    {
        Debug.Log(coroutine);
        coroutineList.Add(coroutine);
    }


    private void Start()
    {
        //loadingBarSlider.gameObject.SetActive(true);
        //string url = "http://ftp.lichenyi.cn/model/Toilet.assetbundle";
        //www = new WWW(url);
        //StartCoroutine(showProgressAndLoadModel(www));

        //WWW www = new WWW(url);
        //StartCoroutine(showProgressAndLoadModel(www, Vector3.one));
        StartCoroutine(showProgressAndLoadModel(UnityWebRequestAssetBundle.GetAssetBundle(url), Vector3.one));
        //WWW www1 = new WWW(url);
        //StartCoroutine(showProgressAndLoadModel1(www1, scale));
        //Dictionary<string, string> result = getModelURLByImageName("logo_adidas_black");
        //Debug.Log(JsonConvert.SerializeObject(result));
        //string url, scaleX, scaleY, scaleZ;
        //result.TryGetValue("url", out url);
        //result.TryGetValue("scale_x", out scaleX);
        //result.TryGetValue("scale_y", out scaleY);
        //result.TryGetValue("scale_z", out scaleZ);
        //scale = new Vector3(Convert.ToSingle(scaleX), Convert.ToSingle(scaleY), Convert.ToSingle(scaleZ));
        //Debug.Log(scale);
        //WWW www = new WWW(url);
        //StartCoroutine(showProgressAndLoadModel(www, scale));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            destoryModel();
            Debug.Log("重新加载");
            //重新加载
            loadingBarSlider.gameObject.SetActive(true);
            //WWW www = new WWW(url);
            //addCoroutineList(StartCoroutine(showProgressAndLoadModel(www, scale)));
            StartCoroutine(showProgressAndLoadModel(UnityWebRequestAssetBundle.GetAssetBundle(url), Vector3.one));
            //WWW www1 = new WWW(url);
            //addCoroutineList(StartCoroutine(showProgressAndLoadModel1(www1, scale)));
        }
    }


    /// <summary>
    /// 显示加载动画,同时加载模型
    /// </summary>
    /// <param name="www"></param>
    /// <returns></returns>
    private IEnumerator showProgressAndLoadModel(WWW www, Vector3 scale)
    {
        showLodingAnimation(); //显示加载动画
        startTimeOfAnimation = Time.time;
        //将动画进度条加载到0.8

        while (!www.isDone)
        {
            if (Time.time - startTimeOfAnimation >= timeStep && loadingBarSlider.gameObject.activeSelf)
            {
                loadingBarSlider.value = www.progress;
                startTimeOfAnimation = Time.time;
            }

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.1f);

        GameObject model = Instantiate(www.assetBundle.mainAsset) as GameObject;
        model.SetActive(false);
        while (loadingBarSlider.value < 1)
        {
            if (Time.time - startTimeOfAnimation >= timeStep)
            {
                loadingBarSlider.value += oneStepValue;
                startTimeOfAnimation = Time.time;
            }

            yield return new WaitForEndOfFrame();
        }

        www.assetBundle.Unload(false);
        displayModel(model, scale); //显示网络模型
    }
    private IEnumerator showProgressAndLoadModel(UnityWebRequest uwr, Vector3 scale)
    {
        yield return uwr.SendWebRequest();
        showLodingAnimation(); //显示加载动画
        startTimeOfAnimation = Time.time;
        //将动画进度条加载到0.8
        while (!uwr.isDone)
        {
            if (Time.time - startTimeOfAnimation >= timeStep && loadingBarSlider.gameObject.activeSelf)
            {
                loadingBarSlider.value = uwr.downloadProgress;
                startTimeOfAnimation = Time.time;
            }

            yield return new WaitForEndOfFrame();
        }

        AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(uwr);
        GameObject model = Instantiate(assetBundle.mainAsset) as GameObject;
        model.SetActive(false);
        while (loadingBarSlider.value < 1)
        {
            if (Time.time - startTimeOfAnimation >= timeStep)
            {
                loadingBarSlider.value += oneStepValue;
                startTimeOfAnimation = Time.time;
            }

            yield return new WaitForEndOfFrame();
        }

        assetBundle.Unload(false);
        displayModel(model, scale); //显示网络模型
    }

    private IEnumerator showProgressAndLoadModel1(WWW www, Vector3 scale)
    {
        while (!www.isDone)
        {
            Debug.Log("加载" + www.progress);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.1f);

        Object model = www.assetBundle.mainAsset;
        GameObject gameObject = model as GameObject;

        GameObject g0 = new GameObject(gameObject.name);
        int count = gameObject.transform.childCount;
        Debug.Log(count);
        for (int i = 0; i < count; i++)
        {
            GameObject g1 = gameObject.transform.GetChild(i).gameObject;
            GameObject g11 = GameObject.Instantiate(g1);
            g11.name = g1.name;
            g11.transform.SetParent(g0.transform);
        }

        www.assetBundle.Unload(false);
    }

    /// <summary>
    /// 显示网络模型
    /// </summary>
    private void displayModel(GameObject model, Vector3 scale)
    {
        //StopCoroutine("showProgressAndLoadModel");
        model.SetActive(true);
        //hideLoadingAnimation(); //隐藏加载动画
        model.transform.position = Vector3.zero;
        model.transform.localScale = scale;
        model.transform.parent = transform;
        //model.transform.parent = modelPanel.transform;
        //model.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        //addComponent<BoxCollider>(model);//添加collider
        //addComponent<ModelRotation>(model);//模型转动
        //addComponent<CommentReplyController>(model);//添加留言UI
        // addComponent<CommentListController>(model);//显示留言列表
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
        int counts = transform.childCount;
        Debug.Log(counts);
        for (int i = 0; i < counts; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void destoryModel()
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
        hideAllModel(); //隐藏所有的动画
        hideLoadingAnimation(); //隐藏加载动画
    }

    /// <summary>
    /// 添加GameObject属性
    /// </summary>
    /// <param name="gameObject"></param>
    /// <typeparam name="T"></typeparam>
    private void addComponent<T>(GameObject gameObject) where T : Component
    {
        T t = gameObject.GetComponent<T>();
        if (t == null)
        {
            gameObject.AddComponent<T>();
        }
    }

    private Dictionary<string, string> getModelURLByImageName(string imageName)
    {
        string querySQL = "select model_upload_info.* from model_upload_info WHERE model_upload_info.url = (SELECT model_url from image_upload_info WHERE image_upload_info.`name` = @name);";
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("@name", imageName);
        Dictionary<string, string> result = new Dictionary<string, string>();
        List<Dictionary<string, string>> queryResult = MySqlHelper.query(querySQL, parameters);
        foreach (Dictionary<string, string> row in queryResult)
        {
            string modelUrl, id, scaleX, scaleY, scaleZ;
            row.TryGetValue("url", out modelUrl);
            row.TryGetValue("id", out id);

            if (modelUrl != null)
                return row;
        }

        return result;
    }
}