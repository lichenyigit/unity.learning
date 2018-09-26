using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMsg1 : MonoBehaviour
{
    void Start()
    {
        SendMessage("ShowMesg", "wyz");
    }
}