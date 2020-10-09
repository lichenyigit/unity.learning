using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullDownList : MonoBehaviour
{
    private List<GameObject> itemPanelList;
    public GameObject itemPanel;


    private void Awake()
    {
        itemPanelList = new List<GameObject>();
    }
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject newItemPanel = Instantiate(itemPanel);
            itemPanelList.Add(newItemPanel);
            newItemPanel.GetComponent<ItemPanelBase>().SetBaseParent(this.transform);
            newItemPanel.GetComponent<ItemPanelBase>().InitPanelContent(new ItemBean("一级菜单" + i, i));
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject newItemPanel2 = Instantiate(itemPanel);
            itemPanelList.Add(newItemPanel2);
            newItemPanel2.GetComponent<ItemPanelBase>().SetItemParent(itemPanelList[i].GetComponent<ItemPanelBase>());
            newItemPanel2.GetComponent<ItemPanelBase>().InitPanelContent(new ItemBean("二级菜单" + i, i));
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject newItemPanel3 = Instantiate(itemPanel);
            itemPanelList.Add(newItemPanel3);
            newItemPanel3.GetComponent<ItemPanelBase>().SetItemParent(itemPanelList[11].GetComponent<ItemPanelBase>());
            newItemPanel3.GetComponent<ItemPanelBase>().InitPanelContent(new ItemBean("三级菜单" + i, i));
        }
    }


    public void Add() {
        for (int i = 0; i < 2; i++)
        {
            GameObject newItemPanel4 = Instantiate(itemPanel);
            itemPanelList.Add(newItemPanel4);
            newItemPanel4.GetComponent<ItemPanelBase>().SetItemParent(itemPanelList[1].GetComponent<ItemPanelBase>());
            newItemPanel4.GetComponent<ItemPanelBase>().InitPanelContent(new ItemBean("二级菜单" + i, i));
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject newItemPanel4 = Instantiate(itemPanel);
                itemPanelList.Add(newItemPanel4);
                newItemPanel4.GetComponent<ItemPanelBase>().SetItemParent(itemPanelList[1].GetComponent<ItemPanelBase>());
                newItemPanel4.GetComponent<ItemPanelBase>().InitPanelContent(new ItemBean("二级菜单" + i, i));
            }
        }
    }
}
