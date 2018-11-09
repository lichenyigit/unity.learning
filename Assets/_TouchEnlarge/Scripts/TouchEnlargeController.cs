using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchEnlargeController : MonoBehaviour
{
    public Text text0;
    public Text text1;

    private Touch touch0;
    private Vector2 startPos0;
    private Touch touch1;
    private Vector2 startPos1;
    private float magnitude0;
    private float magnitude1;

    private Touch oldTouch1;
    private Touch oldTouch2;

    private void Start()
    {
        Vector2 v0 = new Vector2(1, 1);
        Vector2 v1 = new Vector2(0, 0);

        Debug.Log((v0 - v1).magnitude * -1);
    }

    void Update()
    {
        if (Input.touchCount < 1)
            return;


        /*touch0 = Input.GetTouch(0);

        switch (touch0.phase)
        {
            case TouchPhase.Began:
                startPos0 = touch0.position;
                break;

            case TouchPhase.Moved:
                Vector2 resultPos = touch0.position - startPos0;
                magnitude0 = resultPos.magnitude;
                text0.text = magnitude0 + "";
                break;

            case TouchPhase.Ended:
                break;
        }

        touch1 = Input.GetTouch(1);
        switch (touch1.phase)
        {
            case TouchPhase.Began:
                startPos1 = touch1.position;
                break;

            case TouchPhase.Moved:
                text1.text = (touch1.position - startPos1) + "";
                Vector2 resultPos = touch1.position - startPos1;
                magnitude1 = resultPos.magnitude;
                text1.text = magnitude1 + "";
                break;

            case TouchPhase.Ended:
                break;
        }

        if (magnitude0 > 150 && magnitude1 > 150)
        {
            //gameObject.transform.localScale += Mathf.Min(magnitude0, magnitude1) / 1000;
            float unit = Mathf.Min(magnitude0, magnitude1) / 1000;
            gameObject.transform.localScale += (Vector3.one * unit);
        }*/


        Touch newTouch1 = Input.GetTouch(0);
        Touch newTouch2 = Input.GetTouch(1);

        if (newTouch2.phase == TouchPhase.Began && newTouch1.phase == TouchPhase.Began)
        {
            oldTouch2 = newTouch2;
            oldTouch1 = newTouch1;
            return;
        }

        if (newTouch1.phase == TouchPhase.Moved || newTouch2.phase == TouchPhase.Moved)
        {
            float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
            float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

            float offset = newDistance - oldDistance;

            float scaleFactor = offset / 1000f;
            Vector3 localScale = transform.localScale;
            Vector3 scale = localScale + new Vector3(scaleFactor, scaleFactor, scaleFactor);

            if (scale.x > 0.01f && scale.y > 0.01f && scale.z > 0.01f || scale.x < 0.01f && scale.y < 0.01f && scale.z < 0.01f)
            {
                if (scaleFactor < 0)
                {
                    //进行了修改
                    if (transform.localScale.x > 0.1f)
                    {
                        transform.localScale = scale;
                    }
                }
                else
                {
                    transform.localScale = scale;
                }
            }
        }

        oldTouch1 = newTouch1;
        oldTouch2 = newTouch2;
    }
}