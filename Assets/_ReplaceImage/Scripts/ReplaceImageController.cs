using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplaceImageController : MonoBehaviour
{
    public Image image;

    private string imageURL = "http://ftp.lichenyi.cn/img/logo_audi.png";
    
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
//        LoadByIO();
    }

    IEnumerator loadImage()
    {
        WWW www = new WWW(imageURL);
        while (!www.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        Debug.Log("success");
    }
    
    private void LoadByIO()
    {
        double startTime = (double)Time.time;
        //创建文件读取流
        FileStream fileStream = new FileStream(imageURL, FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        //创建文件长度缓冲区
        byte[] bytes = new byte[fileStream.Length];
        //读取文件
        fileStream.Read(bytes, 0, (int)fileStream.Length);
        //释放文件读取流
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;

        //创建Texture
        int width = 300;
        int height = 372;
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(bytes);

        //创建Sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        image.sprite = sprite;

        startTime=(double)Time.time-startTime;
        Debug.Log("IO加载用时:" + startTime);
    }
    
}