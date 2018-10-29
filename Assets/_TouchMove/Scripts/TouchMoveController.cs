using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 单个手指点击移动
 */
public class TouchMoveController : MonoBehaviour
{
    public static TouchMoveController initialized;
    public Text text;
    public bool startMove = false; //

    //手指手势
    private Touch oldTouch1;
    private float startTime = 0;
    private float timeStep = 5f;

    private void Awake()
    {
        if (initialized == null)
        {
            initialized = this;
        }
        else if (initialized != this)
        {
            Destroy(initialized);
        }

        Debug.Log(110);
    }

    void Update()
    {
        move();
    }

    /// <summary>
    /// 模型随着手指移动
    /// </summary>
    private void move()
    {
        text.text = Input.touchCount+"";
        if (Input.touchCount <= 0)
        {
            startMove = false;
            startTime = 0;
            return;
        }

        if (1 == Input.touchCount)
        {
            if (startTime == 0)
            {
                startTime = Time.time;
            }

            Touch touch = Input.GetTouch(0);
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                text.text += "," + (Time.time - startTime >= timeStep).ToString();
                if (Time.time - startTime >= timeStep)
                {
                    startMove = true;
                    Vector2 deltaPos = touch.deltaPosition;
                    if (hit.transform.gameObject.name.Equals(gameObject.name))
                    {
                        transform.position = gameObject.transform.position + new Vector3(deltaPos.x, deltaPos.y, 0) / 92.7f;
                    }
                }
            }
        }
        else
        {
            startTime = 0;
        }
    }
}