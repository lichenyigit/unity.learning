using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBoxColidderController : MonoBehaviour {

	void Start ()
	{
		int count = gameObject.transform.childCount;
		for (int i = 0;i < count;i++)
		{
			gameObject.transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
		}
	}

}
