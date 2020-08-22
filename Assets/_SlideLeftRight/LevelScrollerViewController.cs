using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelScrollerViewController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static LevelScrollerViewController Instance;

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private GameObject pageContent;

    [SerializeField]
    private float pageItemWidth;

    [SerializeField]
    private int minFontSize;

    [SerializeField]
    private int maxFontSize;

    private float originPosX = 0, scrollViewWidth;
    Vector2 destination = new Vector2(0, 0); //目的地pos
    private int moveUnit = 0;


    #region GET SET
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scrollViewWidth = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        parseGameObjectList(pageContent.transform.GetChild(0).gameObject, "Text").GetComponent<Text>().fontSize = maxFontSize;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //当前content POSX
        originPosX = content.GetComponent<RectTransform>().anchoredPosition.x;

        //计算偏移量(获取的是没他item的下标从0开始)
        moveUnit = Mathf.RoundToInt(originPosX / scrollViewWidth);

        //计算目的地
        destination = new Vector2(moveUnit * scrollViewWidth, 0);


        #region page
        selectPage(moveUnit);
        #endregion
    }

    private void Update()
    {
        content.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(content.GetComponent<RectTransform>().anchoredPosition, destination, Time.deltaTime * 10);
    }
    #endregion


    #region PERSON
    /// <summary>
    /// 页码点击事件
    /// </summary>
    /// <param name="page"></param>
    public void pageClick(int page)
    {
        destination = new Vector2(-page * scrollViewWidth, 0);
        selectPage(-page);
    }

    void selectPage(int page)
    {
        int count = pageContent.transform.childCount;

        //当前数字显示最大
        parseGameObjectList(pageContent.transform.GetChild(Mathf.Abs(page)).gameObject, "Text").GetComponent<Text>().fontSize = maxFontSize;

        //设置页码position
        //内容向左滑(页码从小到大)
        if (Mathf.Abs(page) < 3)
        {
            pageContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        else if (Mathf.Abs(page) < count - 2)
        {
            pageContent.GetComponent<RectTransform>().anchoredPosition = new Vector2((page + 2) * pageItemWidth, 0);
        }
        else if (Mathf.Abs(page) > count - 2)
        {
            pageContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(-(count - 5) * pageItemWidth, 0); //因为每排为5个，所以减去5
        }

        //其他数字回复原来大小
        for (int i = 0; i < count; i++)
        {
            if (Mathf.Abs(page) != i)
            {
                parseGameObjectList(pageContent.transform.GetChild(i).gameObject, "Text").GetComponent<Text>().fontSize = minFontSize;
            }
        }
    }
    #endregion


    #region TOOL
    GameObject parseGameObjectList(GameObject gameObject, params string[] names)
    {
        string originName = gameObject.name;
        if (gameObject == null)
        {
            Debug.Log("gameObject is null.");
            return null;
        }

        for (int i = 0; i < names.Length; i++)
        {
            string name = names[i];
            foreach (Transform transform in gameObject.transform)
            {
                string gameObjectName = transform.gameObject.name;
                if (gameObjectName.Equals(name))
                {
                    gameObject = transform.gameObject;
                    break;
                }
            }
        }

        if (originName.Equals(gameObject.name))
            return null;
        return gameObject;
    }
    #endregion
}