using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    

    // Use this for initialization
    void Start()
    {
        Map<string, string> map = new Map<string, string>();
        map.set("1", "1");
        map.set("2", "2");
        map.set("3", "1-3");
        map.set("1", "1-1");
        Debug.Log(map.get("1"));
        Debug.Log(map.get("2"));
        Debug.Log(map.get("3"));
        Debug.Log(map.count);
        Debug.Log(map.removeAll());
        Debug.Log(map.count);
    }

    // Update is called once per frame
    void Update()
    {
    }
}