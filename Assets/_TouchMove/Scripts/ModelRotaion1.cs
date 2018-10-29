using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 模型转动控制器(单个手指控制)
/// </summary>
public class ModelRotaion1 : MonoBehaviour
{
    void Update()
    {
        rotate();
    }

    /// <summary>
    /// 模型旋转
    /// </summary>
    private void rotate()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        if (1 == Input.touchCount)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 deltaPos = touch.deltaPosition;
            if (!TouchMoveController.initialized.startMove)
            {
                transform.Rotate(Vector3.down * deltaPos.x, Space.World);
            }
        }
    }
}