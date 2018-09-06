using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ModelEventController : MonoBehaviour
{
    private void Start()
    {
        gameObject.AddComponent<Button>();
        Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();
        m_OnClick.AddListener(delegate() { Btn_Test(); });
        gameObject.GetComponent<Button>().onClick.AddListener(delegate()
        {
            Btn_Test();
        });
        //m_OnClick.Invoke();
    }

    //按钮点击事件的方法
    void Btn_Test()
    {
        Debug.Log("这是一个按钮点击事件！哈哈");
    }
}