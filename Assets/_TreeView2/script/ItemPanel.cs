﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : ItemPanelBase
{

    public override void InitPanelContent(ItemBeanbase itemBeanbase)
    {
        base.InitPanelContent(itemBeanbase);
        ItemBean itemBean = (ItemBean)itemBeanbase;
        this.transform.Find("ContentPanel/Text").GetComponent<Text>().text = itemBean.name + itemBean.age;
    }
}
