using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicClickController : MonoBehaviour
{

	public GameObject model;
	
	// Use this for initialization
	void Start ()
	{
		//model.AddComponent<ModelEventController>();
		GameObject btnObj = GameObject.Find("Cube");  
		Button btn = btnObj.GetComponent<Button>();
		//注册按钮的点击事件
//		btn.onClick.AddListener(() => Btn_Test());
	}

	//按钮点击事件的方法
	public void Btn_Test() 
	{
		Debug.Log("这是一个按钮点击事件！哈哈");
	}
}
