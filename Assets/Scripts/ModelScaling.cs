using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScaling : MonoBehaviour
{
    private Camera theCamera;

    //距离摄像机8.5米 用黄色表示
    public float upperDistance = 8.5f;

    //距离摄像机12米 用红色表示
    public float lowerDistance = 12.0f;

    private Transform tx;

    void Start()
    {
        theCamera = Camera.main;
        tx = theCamera.transform;
        
//        FindUpperCorners();
//        FindLowerCorners();
    }

    void Update()
    {
        getCornersHeight(Camera.main, upperDistance);
        getObjectSize();
    }

    void FindUpperCorners()
    {
        Vector3[] corners = GetCorners(Camera.main, upperDistance);

        // for debugging
        Debug.DrawLine(corners[0], corners[1], Color.yellow); // UpperLeft -> UpperRight
        Debug.DrawLine(corners[1], corners[3], Color.yellow); // UpperRight -> LowerRight
        Debug.DrawLine(corners[3], corners[2], Color.yellow); // LowerRight -> LowerLeft
        Debug.DrawLine(corners[2], corners[0], Color.yellow); // LowerLeft -> UpperLeft
    }

    void FindLowerCorners()
    {
        Vector3[] corners = GetCorners(Camera.main, lowerDistance);

        // for debugging
        Debug.DrawLine(corners[0], corners[1], Color.red);
        Debug.DrawLine(corners[1], corners[3], Color.red);
        Debug.DrawLine(corners[3], corners[2], Color.red);
        Debug.DrawLine(corners[2], corners[0], Color.red);
    }


    private Vector3[] GetCorners(Camera camera, float distance)
    {
        Vector3[] corners = new Vector3[4];

        // Top left
        corners[0] = camera.ViewportToWorldPoint(new Vector3(0, 1, distance));

        // Top right
        corners[1] = camera.ViewportToWorldPoint(new Vector3(1, 1, distance));

        // Bottom left
        corners[2] = camera.ViewportToWorldPoint(new Vector3(0, 0, distance));

        // Bottom right
        corners[3] = camera.ViewportToWorldPoint(new Vector3(1, 0, distance));
        return corners;
    }

    private float getCornersHeight(Camera camera, float distance)
    {
        // Top left
        Vector3 topLeft = camera.ViewportToWorldPoint(new Vector3(0, 1, distance));

        // Top right
        Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        
        // Bottom left
        Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        
        Debug.Log("width  "+(topRight.x - topLeft.x)+",    "+"height  "+(topLeft.y - bottomLeft.y));
        return 0;
    }

    private void getObjectSize()
    {
        float x = GameObject.Find("Cube").GetComponent<MeshRenderer>().bounds.size.x;
        Debug.Log(x);
    }
}