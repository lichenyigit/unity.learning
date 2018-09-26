using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.UI;

public class CreateGameObjectController : MonoBehaviour
{

    public Font arial;
    public Image bgImage;
    
    void Start()
    {
        createComment();
    }

    private void createComment()
    {
        GameObject commentItem = new GameObject("CommentItem");
        commentItem.transform.parent = transform.parent;
        commentItem.SetActive(true);
        //创建canvas
        {
            GameObject canvas = new GameObject("Canvas");
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            canvas.transform.parent = commentItem.transform;
            canvas.SetActive(true);
            //create panel 
            {
                GameObject panelComment = new GameObject("PanelComment");
                panelComment.AddComponent<CanvasRenderer>();
                panelComment.AddComponent<Image>();
                panelComment.transform.parent = canvas.transform;
                panelComment.SetActive(true);
                //create text
                {
                    GameObject text = new GameObject("Text");
                    text.AddComponent<CanvasRenderer>();
                    text.AddComponent<Text>();
                    text.GetComponent<Text>().font = arial;
                    text.GetComponent<Text>().text = "你好";
                    text.GetComponent<Text>().color = Color.black;
                    text.transform.parent = panelComment.transform;
                    text.SetActive(true);
                }
            }
        }
    }
}