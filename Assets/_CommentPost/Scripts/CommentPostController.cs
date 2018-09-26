using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommentPostController : MonoBehaviour
{

//    public GameObject gg;
    void Start()
    {
        try
        {
            gameObject.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            gameObject.GetComponentInChildren<Button>().onClick.AddListener(delegate { Debug.Log(11); });
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
        }
/*//        GameObject gg1 = GameObject.Instantiate(gg);
        GameObject gg = GameObject.Find("PanelComment");
        gameObject.SetActive(true);
//        GameObject Button = parseGameObjectList(gg, "Canvas", "PanelComment", "Button");
//        Debug.Log(Button.GetComponent<Button>().onClick);
        gameObject.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        gameObject.GetComponentInChildren<Button>().onClick.AddListener(delegate
        {
            Debug.Log(11);
        });*/
    }


    /*private GameObject parseGameObjectList(GameObject gameObject, params string[] names)
    {
        if (gameObject == null)
        {
            Debug.Log("gameObject is null.");
            return null;
        }

        for (int i = 1; i < names.Length; i++)
        {
            string name = names[i];
            gameObject = GameObject.Find(name);
            if (gameObject != null)
            {
                int count = gameObject.transform.childCount;
                for (int j = 0; j < count; j++)
                {
                    string gameObjectName = gameObject.transform.GetChild(j).gameObject.name;
                    Debug.Log(gameObjectName);
                    if (gameObjectName.Equals(name))
                    {
                        gameObject = gameObject.transform.GetChild(j).gameObject;
                    }
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
    }*/
}