using UnityEngine;
using UnityEngine.Events;

public class InvokeTest : MonoBehaviour
{
    UnityEvent m_MyEvent;

    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();
        
        m_MyEvent.AddListener(CommentListController.initiallize.bb);
        //Invoke("GestureController.bb", 0);
        m_MyEvent.Invoke();
    }
}