using UnityEngine;
using UnityEngine.UI;

public class RotateModelController : MonoBehaviour
{
    public Text text;
    private Vector2 v1 = Vector2.zero;

    private void Update()
    {
        Touch touch = Input.GetTouch(0);
        Vector2 deltaPos = touch.deltaPosition;
        transform.Rotate(Vector3.down * deltaPos.x / 10, Space.World);
        transform.Rotate(Vector3.right * deltaPos.y / 10, Space.World);
        v1 = touch.deltaPosition;
    }
}