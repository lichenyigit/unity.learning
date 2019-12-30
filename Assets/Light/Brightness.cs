using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    public Scrollbar scrollbar;

    private void Start()
    {
        scrollbar.onValueChanged.AddListener(SetApplicationBrightnessTo);
    }

    private void SetApplicationBrightnessTo(float Brightness)
    {
        AndroidJavaObject Activity = null;
        Activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        Activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaObject Window = null, Attributes = null;
            Window = Activity.Call<AndroidJavaObject>("getWindow");
            Attributes = Window.Call<AndroidJavaObject>("getAttributes");
            Attributes.Set("screenBrightness", Brightness);
            Window.Call("setAttributes", Attributes);
        }));
    }

    
}