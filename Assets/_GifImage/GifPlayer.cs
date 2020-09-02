using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing; //需要下载这个dll放到工程中
using System.Drawing.Imaging;
using System.IO;
using System;

/// <summary>
/// Gif --> Bitmap --> Stream --> Byte数组 --> Texture2D对象
/// </summary>
public class GifPlayer : MonoBehaviour
{
    private System.Drawing.Image gifImage; //Gif图片

    [SerializeField]
    private UnityEngine.UI.Image image; //显示Gif的UI

    private float fps = 10;
    private List<Texture2D> text2DList; //Gif拆成的图片
    private float time;
    private int countUp = 0;
    Bitmap myBitmap;

    private void Start()
    {
        gifImage = Image.FromFile(Application.streamingAssetsPath + "/background.gif");
        text2DList = GifToTexture2D(gifImage);
    }

    private void Update()
    {
        if (text2DList.Count > 0)
        {
            time += Time.deltaTime;

            //速度控制
            if (time > 1f)
            {
                int index = countUp % text2DList.Count;
                if (image != null)
                {
                    Sprite sprite = Sprite.Create(text2DList[index], new Rect(0, 0, text2DList[index].width, text2DList[index].height), new Vector2(0.5f, 0.5f));
                    image.overrideSprite = sprite;
                }

                time = 0;
                countUp++;
            }
        }
    }

    /// <summary>
    /// gif to List<Texture2D>
    /// </summary>
    /// <param name="gifImage"></param>
    /// <returns></returns>
    List<Texture2D> GifToTexture2D(System.Drawing.Image gifImage)
    {
        List<Texture2D> target = null;
        if (gifImage != null)
        {
            target = new List<Texture2D>();

            //图片的构成有两种形式：1.多页（.gif） 2.多分辨率
            Debug.Log("dimenson:" + gifImage.FrameDimensionsList.Length);

            //根据指定的GUID(gifImage.FrameDimensionsList[0])
            //创建一个提供获取图像框架维度信息的实例
            FrameDimension frameDimension = new FrameDimension(gifImage.FrameDimensionsList[0]);

            int frameCount = gifImage.GetFrameCount(frameDimension);
            for (int i = 0; i < frameCount; i++)
            {
                gifImage.SelectActiveFrame(frameDimension, i);

                //创建Bitmap的实例
                Bitmap frameBitmap = new Bitmap(gifImage.Width, gifImage.Height);

                //将GifImage通过Graphics画到Bitmap上
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(frameBitmap))
                {
                    graphics.DrawImage(gifImage, Point.Empty);
                }

                //创建Texture2D的实例
                Texture2D frameTexture2D = new Texture2D(frameBitmap.Width, frameBitmap.Height, TextureFormat.ARGB32, true);
                frameTexture2D.LoadImage(Bitmap2Byte(frameBitmap));
                target.Add(frameTexture2D);
            }
        }
        Debug.Log("编译完成");
        return target;
    }

    /// <summary>
    /// bitmap to byte
    /// </summary>
    /// <param name="bitmap"></param>
    /// <returns></returns>
    private byte[] Bitmap2Byte(Bitmap bitmap)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            // 将bitmap 以png格式保存到流中
            bitmap.Save(stream, ImageFormat.Png);

            // 创建一个字节数组，长度为流的长度
            byte[] data = new byte[stream.Length];

            // 重置指针
            stream.Seek(0, SeekOrigin.Begin);

            // 从流读取字节块存入data中
            stream.Read(data, 0, Convert.ToInt32(stream.Length));
            return data;
        }
    }
}