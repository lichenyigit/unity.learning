using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplaceImageController : MonoBehaviour
{
    public Image image;

    void Start()
    {
    }

    public void changeScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void changeBackground()
    {
        StartCoroutine(loadImage());
    }

    IEnumerator loadImage()
    {
        WWW www = new WWW("http://ftp.lichenyi.cn/img/000.jpg");
        yield return www;
        image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        Debug.Log("success");
    }
}