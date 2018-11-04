using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    private GameObject gObj = null;
    private Plane objPlane;
    private Vector3 m0;

    Ray GenerateMouseRay()
    {
        Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);
        Ray mr = new Ray(mousePosN, mousePosF - mousePosN);
        return mr;
    }

    void Update()
    {
        RaycastHit hit0 = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //判断射线是否射到了物体
        if (Physics.Raycast(ray, out hit0, 1000))
        {
            if (hit0.transform.gameObject.name.Equals(gameObject.name))
            {
                //拖动物体
                if (Input.GetMouseButtonDown(0))
                {
                    Ray mouseRay = GenerateMouseRay();
                    RaycastHit hit;
                    if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit))
                    {
                        gObj = hit.transform.gameObject;
                        objPlane = new Plane(Camera.main.transform.forward * -1, gObj.transform.position);
                        //计算鼠标移动的位置
                        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                        float rayDistance;
                        objPlane.Raycast(mRay, out rayDistance);
                        m0 = gObj.transform.position - mRay.GetPoint(rayDistance);
                    }
                }
                else if (Input.GetMouseButton(0) && gObj)
                {
                    Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    float rayDistance;
                    if (objPlane.Raycast(mRay, out rayDistance))
                    {
                        gObj.transform.position = mRay.GetPoint(rayDistance) + m0;
                    }
                }
                else if (Input.GetMouseButtonUp(0) && gObj)
                {
                    gObj = null;
                }
            }
        }
        
    }
}