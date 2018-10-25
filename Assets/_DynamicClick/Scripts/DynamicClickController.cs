using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DynamicClickController : MonoBehaviour
{
    public GameObject buton;

    void Start()
    {
        //model.AddComponent<ModelEventController>();
//		GameObject btnObj = GameObject.Find("Cube");  
//		Button btn = btnObj.GetComponent<Button>();
        //注册按钮的点击事件
//		btn.onClick.AddListener(() => Btn_Test());


//        GameObject canvas = GameObject.Find("Canvas");
//        GameObject p = canvas.transform.GetChild(0).gameObject;
        //GameObject p = parseGameObjectList(GameObject.Find("Canvas"), "Panel");
//        GameObject p = getGameObject("Canvas", "Panel");

        /*for (int i = 0; i < 3; i++)
        {
            GameObject p = Instantiate(c);
            Debug.Log(i + " " + p.GetHashCode());
            p.name += p.GetHashCode();
            p.transform.parent = c.transform.parent;
            p.transform.position = Vector3.zero;
            p.AddComponent<TouchMoveController>();
            p.SetActive(true);

            
            GameObject text = Utils.initialized.parseGameObjectList(p, "Canvas", "PanelComment", "Text");
            text.GetComponent<Text>().text = i + "";
            GameObject g = Utils.initialized.parseGameObjectList(p, "Canvas", "PanelComment", "ButtonReply");
            addClick(g, delegate { p.SetActive(false); });
        }*/
        
        
        addClick(buton, delegate
        {
            Debug.Log(11);
        });
        addClick(buton, delegate
        {
            Debug.Log(22);
        });
    }

    //按钮点击事件的方法
    public void Btn_Test()
    {
        Debug.Log("这是一个按钮点击事件！哈哈");
    }

    public void addClick(GameObject gameObject, UnityAction call)
    {
        if (gameObject.GetComponent<Button>() == null)
        {
            gameObject.AddComponent<Button>();
        }

        gameObject.GetComponent<Button>().onClick.AddListener(call);
    }
    
}