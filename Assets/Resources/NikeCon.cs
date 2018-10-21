using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikeCon : MonoBehaviour
{

	private GameObject[] products;
	private int index;
	

	void Start () {
		products = new GameObject[transform.childCount];
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				products[i] = transform.GetChild(i).gameObject;
			}

			foreach (GameObject go in products)
			{
				go.SetActive(false);
			}

			if (products[0])
			{
				products[0].SetActive(true);
			}
		}
	}


	public void ToggleSelectLeftRight(bool isLeft)
	{
		products[index].SetActive(false);
		if (isLeft)
		{
			index--;
			if (index < 0)
			{
				index = products.Length - 1;
			}
			products[index].SetActive(true);
		}

		if (!isLeft)
		{
			index++;
			if (index == products.Length)
			{
				index = 0;
			}
			products[index].SetActive(true);
			
		}
	}

}