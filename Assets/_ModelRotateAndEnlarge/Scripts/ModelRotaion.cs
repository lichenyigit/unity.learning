using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 模型转动控制器(单个手指控制)
/// </summary>
public class ModelRotaion : MonoBehaviour
{
    //手指手势
    private Touch oldTouch1;
    private Touch oldTouch2;

    void Update()
    {
        scaleRotate();
    }

    /// <summary>
    /// 模型旋转
    /// </summary>
    private void scaleRotate()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        if (1 == Input.touchCount)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 deltaPos = touch.deltaPosition;

            transform.Rotate(Vector3.down * deltaPos.x, Space.World);
            //transform.Rotate (Vector3.right * deltaPos.y, Space.World);
        }

        Touch newTouch1 = Input.GetTouch(0);
        Touch newTouch2 = Input.GetTouch(1);

        if (newTouch2.phase == TouchPhase.Began)
        {
            oldTouch2 = newTouch2;
            oldTouch1 = newTouch1;
            return;
        }

        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

        float offset = newDistance - oldDistance;

        float scaleFactor = offset / 100f;
        Vector3 localScale = transform.localScale;
        Vector3 scale = new Vector3(localScale.x + scaleFactor, localScale.y + scaleFactor, localScale.z + scaleFactor);

        if (scale.x > 0.1f && scale.y > 0.1f && scale.z > 0.1f || scale.x < 2f && scale.y < 2f && scale.z < 2f)
        {
            //进行了修改
            transform.localScale = scale;
        }

        oldTouch1 = newTouch1;
        oldTouch2 = newTouch2;
    }
}