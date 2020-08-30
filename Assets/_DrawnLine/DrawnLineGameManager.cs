using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawnLineGameManager : MonoBehaviour
{
    
    [SerializeField]
    private Material material;
 
    [SerializeField]
    private GameObject[] gameObjects;

    void Start()
    {
        GameObject gameObject0 = new GameObject();
        gameObject0.name = "Line1";
        gameObject0.AddComponent<LineController>().Exe(gameObjects[0], gameObjects[1]);
        GameObject gameObject1 = new GameObject();
        gameObject1.name = "Line2";
        gameObject1.AddComponent<LineController>().Exe(gameObjects[2], gameObjects[3]);
    }
}
