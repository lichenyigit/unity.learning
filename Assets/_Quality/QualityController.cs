using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityController : MonoBehaviour
{
    public void SetQuality(int index)
    {
        /*for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            Debug.Log(QualitySettings.names[i].ToString());
        }*/
        QualitySettings.SetQualityLevel(index, true);
        Debug.Log(QualitySettings.names[index].ToString());
    }
}
