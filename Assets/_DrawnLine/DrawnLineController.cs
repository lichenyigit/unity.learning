using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DrawnLineController : MonoBehaviour
{
    private GameObject startGO;
    private GameObject endGO;
    private Color color = Color.white;
    private Material material;
    private LineRenderer lineRenderer;
    private bool onEnable = false;

    public void Exe(GameObject startGO, GameObject endGO)
    {
        this.startGO = startGO;
        this.endGO = endGO;

        //添加LineRenderer
        if (gameObject.GetComponent<LineRenderer>() == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        else
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        //设置开始和结束线条宽度
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 4;

        //设置线条颜色
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        //设置材质
        lineRenderer.material = material;

        lineRenderer.useWorldSpace = true;
        lineRenderer.numCapVertices = 50;
        lineRenderer.numCornerVertices = 30;

        onEnable = true;
    }

    private void Update()
    {
        if (onEnable)
        {
            #region 计算中间两点的位置
            //计算Y轴中点(Y轴距离的一半)

            float distanceY = Camera.main.ScreenToWorldPoint(startGO.transform.position).y - Camera.main.ScreenToWorldPoint(endGO.transform.position).y;
            float halfDistanceY = distanceY / 2;

            //计算第一个点的位置
            Vector3 origin1 = Camera.main.ScreenToWorldPoint(startGO.transform.position);
            origin1 = new Vector3(origin1.x, origin1.y, 0);
            Vector3 middle1 = new Vector3(origin1.x, origin1.y - halfDistanceY, 0);

            //计算第二个点的位置
            Vector3 origin2 = Camera.main.ScreenToWorldPoint(endGO.transform.position);
            origin2 = new Vector3(origin2.x, origin2.y, 0);
            Vector3 middle2 = new Vector3(origin2.x, origin2.y + halfDistanceY, 0);

            //划线
            lineRenderer.SetPosition(0, origin1);
            lineRenderer.SetPosition(1, middle1);
            lineRenderer.SetPosition(2, middle2);
            lineRenderer.SetPosition(3, origin2);
            #endregion
        }
    }
}