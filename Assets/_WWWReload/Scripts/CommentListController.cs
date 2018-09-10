using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/**
 * 留言列表控制器
 */
public class CommentListController : MonoBehaviour
{
    public static CommentListController initiallize;
    private GameObject model = null;
    private string commentPostURL = "http://ftp.lichenyi.cn/model/CommentPost.assetbundle";
    private List<Dictionary<string, string>> commentList;
    private List<GameObject> gameObjectList = new List<GameObject>();

    private void Awake()
    {
        if (initiallize == null)
        {
            initiallize = this;
        }else if (initiallize != this)
        {
            Destroy(initiallize);
        }
    }

    private void Start()
    {
        WWW www = new WWW(commentPostURL);
        StartCoroutine(loadModel(www));
    }

    public void refresh()
    {
        Debug.Log("refresh");
        foreach (GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        getCommentList();
    }

    /// <summary>
    /// 显示加载动画,同时加载模型
    /// </summary>
    /// <param name="www"></param>
    /// <returns></returns>
    private IEnumerator loadModel(WWW www)
    {
        yield return www;
        model = Instantiate(www.assetBundle.mainAsset) as GameObject;
        model.SetActive(false);
        www.assetBundle.Unload(false);
        getCommentList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model">显示的模型</param>
    /// <param name="content">留言内容</param>
    /// <param name="position">模型显示的位置</param>
    private void displayModel(GameObject model, string content, Vector3 position)
    {
        model.SetActive(true);
        model.transform.parent = transform.parent;
        model.transform.localPosition = position;
        model.transform.localScale = Vector3.one * 0.8f;
        model.GetComponentInChildren<Text>().text = content;
    }

    private void getCommentList()
    {
        string querySQL = "select * from comment_info ";
        commentList = MySqlHelper.query(querySQL, null);
        if (commentList != null && model != null)
        {
            for (int i = 0; i < commentList.Count; i++)
            {
                Dictionary<string, string> row = commentList[i];
                string content;
                row.TryGetValue("content", out content);
                if (content != null)
                {
                    GameObject model1 = Instantiate(model);
                    displayModel(model1, content, (Vector3.up * 2.5f + Vector3.left) * 2.5f + (Vector3.down * 2f) * i); //显示留言列表  
                    gameObjectList.Add(model1);
                }
            }
        }
    }

}