using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 手势控制
 */
public class GestureController : MonoBehaviour
{
    private Vector2 touchFirst = Vector2.zero; //手指开始按下的位置

    private Vector2 touchSecond = Vector2.zero; //手指拖动的位置

    void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            //判断当前手指是按下事件

            touchFirst = Event.current.mousePosition; //记录开始按下的位置
        }

        if (Event.current.type == EventType.MouseDown)
        {
            //判断当前手指是拖动事件

            touchSecond = Event.current.mousePosition; //记录拖动的位置

            if (touchSecond.x < touchFirst.x)
            {
                //拖动的位置比按下的位置的x小

                //向左滑动
                Debug.Log("向左滑动");
            }

            if (touchSecond.x > touchFirst.x)
            {
                //拖动的位置比按下的位置的x大

                //向右滑动
                Debug.Log("向右滑动");
            }

            touchFirst = touchSecond;
        }
    }
}