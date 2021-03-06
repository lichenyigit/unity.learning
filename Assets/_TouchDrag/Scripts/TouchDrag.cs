﻿using UnityEngine;

public class TouchDrag : MonoBehaviour
{
    private Plane objPlane;
    private Vector3 m0;

    private Vector2 startPos;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0); //获取触摸点
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            //判断射线是否射到了物体
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.transform.gameObject.name.Equals(gameObject.name))
                {
                    Ray mRay;
                    float rayDistance;
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            startPos = touch.position;
                            objPlane = new Plane(Camera.main.transform.forward * -1, gameObject.transform.position);
                            //计算鼠标移动的位置
                            mRay = Camera.main.ScreenPointToRay(touch.position);

                            objPlane.Raycast(mRay, out rayDistance);
                            m0 = gameObject.transform.position - mRay.GetPoint(rayDistance);
                            break;

                        case TouchPhase.Moved:
                            mRay = Camera.main.ScreenPointToRay(touch.position);
                            if (objPlane.Raycast(mRay, out rayDistance))
                            {
                                gameObject.transform.position = mRay.GetPoint(rayDistance) + m0;
                            }

                            break;

                        case TouchPhase.Ended:
                            startPos = Vector2.zero;
                            break;
                    }
                }
            }
        }
    }
}