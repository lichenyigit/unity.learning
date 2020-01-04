using UnityEngine;

public class ColorSelectionPlate : MonoBehaviour
{
    public ColorPicker picker;

    void Start()
    {
        picker.onValueChanged.AddListener(color =>
        {
            float r = color.r * 255;
            float g = color.g * 255;
            float b = color.b * 255;
            float a = color.a * 255;
            Debug.Log(r + "    " + g + "    " + b + "    " + a);
        });
    }
}