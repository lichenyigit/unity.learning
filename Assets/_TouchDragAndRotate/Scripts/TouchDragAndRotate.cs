using UnityEngine;
using UnityEngine.UI;

public class TouchDragAndRotate : MonoBehaviour
{
    private Plane objPlane;
    private Vector3 offsetPos;

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
                            objPlane = new Plane(Camera.main.transform.forward * -1, gameObject.transform.position);
                            //计算鼠标移动的位置
                            mRay = Camera.main.ScreenPointToRay(touch.position);

                            objPlane.Raycast(mRay, out rayDistance);
                            offsetPos = gameObject.transform.position - mRay.GetPoint(rayDistance);
                            break;

                        case TouchPhase.Moved:
                            mRay = Camera.main.ScreenPointToRay(touch.position);
                            if (objPlane.Raycast(mRay, out rayDistance))
                            {
                                gameObject.transform.position = mRay.GetPoint(rayDistance) + offsetPos;
                            }

                            break;

                        case TouchPhase.Ended:
                            break;
                    }
                }
            }
            else
            {
                Vector2 deltaPos = touch.deltaPosition; //获取触摸点自上次以来 更改的增量
                transform.Rotate(Vector3.down * deltaPos.x / 10, Space.World);
            }
        }
    }
}