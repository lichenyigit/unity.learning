using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

/**
 * 留言回复
 */
public class CommentReplyController : MonoBehaviour
{
    
    private string replyURL = "http://ftp.lichenyi.cn/model/Reply.assetbundle";

    private void OnMouseDown()
    {
        WWW www = new WWW(replyURL);
        StartCoroutine(loadModel(www));
    }

    /// <summary>
    /// 显示加载动画,同时加载模型
    /// </summary>
    /// <param name="www"></param>
    /// <returns></returns>
    private IEnumerator loadModel(WWW www)
    {
        yield return www;
        GameObject model = Instantiate(www.assetBundle.mainAsset) as GameObject;
        www.assetBundle.Unload(false);
        displayModel(model, Vector3.zero); //显示留言框
    }

    /// <summary>
    /// 显示网络模型
    /// </summary>
    private void displayModel(GameObject model, Vector3 position)
    {
        model.SetActive(true);
        model.transform.parent = GameObject.Find("Canvas").transform;
        model.transform.localPosition = position;
        model.transform.localScale = Vector3.one * 0.8f;
        //添加提交事件
        model.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate
        {
            string content = model.GetComponentInChildren<InputField>().text;
            if (content.Trim().Length > 0)
            {
                string insertSQL = "insert into comment_info(content) values(@content)";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@content", content);
                MySqlHelper.insertOrUpdate(insertSQL, parameters);
                Destroy(model);
                CommentListController.initiallize.refresh();
            }
        });
        model.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate
        {
            Destroy(model);
        });
    }

    
    
    
    /// <summary>
    /// GameObject 添加属性
    /// </summary>
    /// <param name="gameObject"></param>
    /// <typeparam name="T"></typeparam>
    private void addComponent<T>(GameObject gameObject) where T : Component
    {
        T t = gameObject.GetComponent<T>();
        if (t == null)
        {
            gameObject.AddComponent<T>();
            Debug.Log("添加完毕");
        }
    }
}