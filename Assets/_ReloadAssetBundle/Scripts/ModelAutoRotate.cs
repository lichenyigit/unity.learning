using UnityEngine;

public class ModelAutoRotate : MonoBehaviour
{
    public bool x;
    public bool y;
    public bool z;

    private Vector3 rot;
    public float speed = 10;

    void Update()
    {
        if (x)
        {
            rot = Vector3.right * speed;
        }
        else if (y)
        {
            rot = Vector3.up * speed;
        }
        else if (z)
        {
            rot = Vector3.forward * speed;
        }

        gameObject.transform.Rotate(rot);
    }
}