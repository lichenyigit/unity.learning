using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadImgDBController : MonoBehaviour
{
	//private string imgDBURL = "D:\\_ImageDatabase\\myimages.imgdb";
	private string imgDBURL = "http://ftp.lichenyi.cn/database/myimages.imgdb";
	
	void Start () {
		Debug.Log(Time.time);
		/*byte[] m_RawData = File.ReadAllBytes(imgDBURL);
		byteToHexStr(m_RawData);*/
		WWW www = new WWW(imgDBURL);
		StartCoroutine(loadDatabase(www));
	}
	
	private IEnumerator loadDatabase(WWW www)
	{
		yield return www;
		byteToHexStr(www.bytes);
	}
	
	public static string byteToHexStr(byte[] bytes)
	{
		string returnStr = "";
		if (bytes != null)
		{
			Debug.Log(bytes.Length);
			for (int i = 0; i < bytes.Length; i++)
			{
				//string re = bytes[i].ToString("X2").ToLower();
				string re = Convert.ToString(bytes[i], 16).ToLower();
				returnStr += re;
				//Debug.Log(re);
			}
		}
		Debug.Log(Time.time);
		return returnStr;
	}
	
}
