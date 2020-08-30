using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitchImageController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Sprite[] Sprites;

    [SerializeField]
    private Image image;

    private float fadeOutTimer;

    private int index = 0;

    public void SwitchImage()
    {
        index++;
        if (index == Sprites.Length - 1)
        {
            index = 0;
        }

        StartCoroutine(SwitchIEnumerator());
    }

    IEnumerator SwitchIEnumerator()
    {
        for (int i = 0; i < 255; i++)
        {
            image.color = new Color(1, 1, 1, (255 - i) / 255f);
            yield return new WaitForSecondsRealtime(0.0001f);
        }

        yield return new WaitForSecondsRealtime(0.1f);
        image.sprite = Sprites[index];
        for (int i = 0; i < 255; i++)
        {
            image.color = new Color(1, 1, 1, i / 255f);
            yield return new WaitForSecondsRealtime(0.0001f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SwitchImage();
    }
}