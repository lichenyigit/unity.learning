using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentaryArithmeticController : MonoBehaviour
{
    public GameObject item;

    private Vector3 itemSize;
    private Vector3 startVector3;
    private Vector3 endVector3;

    // Use this for initialization
    void Start()
    {
        
        itemSize = item.GetComponent<BoxCollider>().size;
        startVector3 = new Vector3(900 * 1, 220 * 3, 1) * -1;
        endVector3 = new Vector3(900 * 1, 220 * 3, 1);

        for (int i = 0; i < 5; i++)
        {
            GameObject ite = Instantiate(item);
            Vector3 position = fragmentary(startVector3, endVector3, itemSize);
            ite.transform.position = position;
            ite.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Random.Range(0,2));
    }

    private Vector3 fragmentary(Vector3 startVector3, Vector3 endVector3, Vector2 gameObjectSize)
    {
        Vector3 result = new Vector3();
        bool sign = true;
        do
        {
            result.x = Random.Range(startVector3.x, endVector3.x);
            float halfWidth = gameObjectSize.x / 2;
            if (Mathf.Abs(gameObjectSize.x + halfWidth) < Mathf.Abs(result.x) || Mathf.Abs(gameObjectSize.x - halfWidth) < Mathf.Abs(result.x))
            {
                result.y = Random.Range(startVector3.y, endVector3.y);
                float halfHeight = gameObjectSize.y / 2;
                if (Mathf.Abs(gameObjectSize.y + halfHeight) < Mathf.Abs(result.y) || Mathf.Abs(gameObjectSize.y - halfHeight) < Mathf.Abs(result.y))
                {
                    sign = false;
                }
            }
        } while (sign);
        return result;
    }
}