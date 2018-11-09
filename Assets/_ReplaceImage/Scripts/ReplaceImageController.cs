using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplaceImageController : MonoBehaviour
{
    public Image image;

    private string imageURL = "http://ftp.lichenyi.cn/img/picture_monalisa.jpg";

    private float imageRectHeight;
    private float imageRectWidth;

    void Start()
    {
        imageRectHeight = image.GetComponent<RectTransform>().rect.height;
        imageRectWidth = image.GetComponent<RectTransform>().rect.width;
    }

    public void changeScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void changeBackground()
    {
        StartCoroutine(loadImage());
//        LoadByIO();
    }

    IEnumerator loadImage()
    {
        WWW www = new WWW(imageURL);
        while (!www.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        dengbili(www.texture.width, www.texture.height);
        image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        //image.SetNativeSize();
//        image.GetComponent<RectTransform>().rect.height = wh.x;
        //      image.GetComponent<RectTransform>().w = wh.y;
        Debug.Log("success");
    }

    private void dengbili(float width, float height)
    {
        /*if (width < height)
        {
            image.transform.localScale = new Vector3(width/height, 1, 1);
        }else if (width > height)
        {
            image.transform.localScale = new Vector3(1, height/width, 1);
        }*/
        //如果图片实际大小比image小
        if (width < imageRectWidth || height < imageRectHeight)
        {
            if (width < height)
            {
                image.transform.localScale = new Vector3(width / height, 1, 1);
            }
            else if (width > height)
            {
                image.transform.localScale = new Vector3(1, height / width, 1);
            }
        }
        else if (width > imageRectWidth || height > imageRectHeight) //图片实际大小比image大
        {
            Debug.Log(1);
            if (Mathf.Abs(imageRectWidth - width) < Mathf.Abs(imageRectHeight - height))
            {
                image.transform.localScale = new Vector3(imageRectWidth * width / height, 1, 1);
            }
            else if (Mathf.Abs(imageRectWidth - width) > Mathf.Abs(imageRectHeight - height))
            {
                image.transform.localScale = new Vector3(1, imageRectHeight * height / width, 1);
            }
        }
    }
}