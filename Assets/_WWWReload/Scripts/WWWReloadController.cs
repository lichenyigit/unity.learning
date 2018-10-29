using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class WWWReloadController : MonoBehaviour
{
    //模型网络地址
    private string modelNameFormat = "http://ftp.lichenyi.cn/model/{0}.assetbundle";

    //模型名称
    private string[] modelName = {"001", "002"};
    private Dictionary<int, GameObject> modelOfIndexDictionary = new Dictionary<int, GameObject>();

    public Slider loadingBarSlider;

    public GameObject modelPanel;
    
    //模型集合
    private float startTimeOfAnimation; //程序开始时间
    private float timeStep = 0.05f;
    private float oneStepValue = 0.03f;
    private WWW www;
    private Vector3 scale;

    private string url;
    
    private void Start()
    { 
        /*loadingBarSlider.gameObject.SetActive(true);
        //string url = String.Format(modelNameFormat, modelName[0]);
        string url = "http://ftp.lichenyi.cn/model/Toilet.assetbundle";
        www = new WWW(url);
        StartCoroutine(showProgressAndLoadModel(www));*/
        
        string url = "http://ftp.lichenyi.cn/model/DicosCup.assetbundle";
        www = new WWW(url);
        StartCoroutine(showProgressAndLoadModel(www, Vector3.one));
        
        /*Dictionary<string, string> result = getModelURLByImageName("logo_adidas_black");
        Debug.Log(JsonConvert.SerializeObject(result));
        string url, scaleX, scaleY, scaleZ;
        result.TryGetValue("url", out url);
        result.TryGetValue("scale_x", out scaleX);
        result.TryGetValue("scale_y", out scaleY);
        result.TryGetValue("scale_z", out scaleZ);
        scale = new Vector3(Convert.ToSingle(scaleX), Convert.ToSingle(scaleY), Convert.ToSingle(scaleZ));
        Debug.Log(scale);
        WWW www = new WWW(url);
        StartCoroutine(showProgressAndLoadModel(www, scale));*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("重新加载");
            destoryModel();
            
            //重新加载
            loadingBarSlider.gameObject.SetActive(true);
            WWW www = new WWW(url);
            StartCoroutine(showProgressAndLoadModel(www, scale));
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
        while (!www.isDone && loadingBarSlider.value < 0.8)
        {
            if (Time.time - startTimeOfAnimation >= timeStep && loadingBarSlider.gameObject.active)
            {
                loadingBarSlider.value += oneStepValue;
                startTimeOfAnimation = Time.time;
            }

            yield return new WaitForEndOfFrame();
        }

        while (!www.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        GameObject model = Instantiate(www.assetBundle.mainAsset) as GameObject;
        model.SetActive(false);
        yield return new WaitForSeconds(2);
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

        www.assetBundle.Unload(false);
        displayModel(model, scale); //显示网络模型
    }

    /// <summary>
    /// 显示网络模型
    /// </summary>
    private void displayModel(GameObject model, Vector3 scale)
    {
        model.SetActive(true);
        hideLoadingAnimation(); //隐藏加载动画
        model.transform.position = Vector3.zero;
        model.transform.localScale = scale;
        model.transform.parent = transform;
        //model.transform.parent = modelPanel.transform;
        //model.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        addComponent<BoxCollider>(model);//添加collider
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
        for (int i = 0;i < counts;i++)
        {
            Destroy(transform.GetChild(i).gameObject);    
        }
        
    }

    public void destoryModel()
    {
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
        //string querySQL = "select model_url from image_upload_info where name = @name";
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