using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollederController : MonoBehaviour
{
    public GameObject cube1;

    void Start()
    {
        cube1.GetComponent<Rigidbody>().AddForce(Vector3.right * 30);
    }

    private void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        
        cube1.GetComponent<Rigidbody>().AddForce(new Vector3(h, 0.5f, v) * 2);
    }
}