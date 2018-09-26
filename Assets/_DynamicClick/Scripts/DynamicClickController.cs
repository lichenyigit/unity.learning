using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DynamicClickController : MonoBehaviour
{
    public GameObject c;

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

        for (int i = 0; i < 3; i++)
        {
            GameObject p = Instantiate(c);
            Debug.Log(i + " " + p.GetHashCode());
            p.name += p.GetHashCode();
            p.transform.parent = c.transform.parent;
            p.transform.position = Vector3.zero;
            p.AddComponent<TouchMoveController>();
            p.SetActive(true);

            GameObject text = parseGameObjectList(p, "Canvas", "PanelComment", "Text");
            text.GetComponent<Text>().text = i + "";
            GameObject g = parseGameObjectList(p, "Canvas", "PanelComment", "ButtonReply");
            addClick(g, delegate { p.SetActive(false); });
        }
    }

    //按钮点击事件的方法
    public void Btn_Test()
    {
        Debug.Log("这是一个按钮点击事件！哈哈");
    }

    private GameObject getGameObject(params string[] names)
    {
        GameObject gameObject = null;
        for (int i = 0; i < names.Length; i++)
        {
            string name = names[i];
            gameObject = GameObject.Find(name);
            if (i == 0)
                continue; //第一次循环这里截至

            if (gameObject != null)
            {
                int count = gameObject.transform.childCount;
                for (int j = 0; j < count; j++)
                {
                    string gameObjectName = gameObject.transform.GetChild(j).gameObject.name;
                    if (gameObjectName.Equals(name))
                    {
                        gameObject = gameObject.transform.GetChild(j).gameObject;
                    }
                }
            }
        }

        return gameObject;
    }

    private GameObject parseGameObjectList(GameObject gameObject, params string[] names)
    {
        Debug.Log("parseGameObjectList");
        if (gameObject == null)
        {
            Debug.Log("gameObject is null.");
            return null;
        }

        for (int i = 0; i < names.Length; i++)
        {
            string name = names[i];
            int count = gameObject.transform.childCount;

            for (int j = 0; j < count; j++)
            {
                Debug.Log(i + " " + count + " " + gameObject);
                string gameObjectName = gameObject.transform.GetChild(j).gameObject.name;
                if (gameObjectName.Equals(name))
                {
                    gameObject = gameObject.transform.GetChild(j).gameObject;
                    if (i == (names.Length - 1))
                    {
                        return gameObject;
                    }

                    break;
                }
            }
        }

        return gameObject;
    }

    private void addClick(GameObject gameObject, UnityAction call)
    {
        if (gameObject.GetComponent<Button>() == null)
        {
            gameObject.AddComponent<Button>();
        }

        gameObject.GetComponent<Button>().onClick.AddListener(call);
    }
}