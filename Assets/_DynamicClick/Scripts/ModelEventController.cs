using UnityEngine;
using UnityEngine.UI;

public class ModelEventController : MonoBehaviour
{
    private void Start()
    {
    }

    private void OnMouseDown()
    {
        GameObject.Find("Text").GetComponent<Text>().text = "onmousedown";
        Debug.Log("onmousedown");
    }
}