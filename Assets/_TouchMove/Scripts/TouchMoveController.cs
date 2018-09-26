using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 单个手指点击移动
 */
public class TouchMoveController : MonoBehaviour
{
    //手指手势
    private Touch oldTouch1;

    void Update()
    {
        move();
    }

    /// <summary>
    /// 模型随着手指移动
    /// </summary>
    private void move()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        if (1 == Input.touchCount)
        {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Vector2 deltaPos = touch.deltaPosition;
                if (hit.transform.gameObject.name.Equals(gameObject.name))
                {
                    transform.position = gameObject.transform.position + new Vector3(deltaPos.x, deltaPos.y, 0) / 92.7f;
                }
            }
        }
    }
}