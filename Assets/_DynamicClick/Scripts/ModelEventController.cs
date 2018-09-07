using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModelEventController : MonoBehaviour, IPointerClickHandler
{
    private void Start()
    {
    }

    private void OnMouseDown()
    {
        GameObject.Find("Text").GetComponent<Text>().text = "onmousedown";
        Debug.Log("onmousedown");
    }

    //按钮点击事件的方法
    void Btn_Test()
    {
        Debug.Log("这是一个按钮点击事件！哈哈");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Btn_Test();
    }
}