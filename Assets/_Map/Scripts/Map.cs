using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map<TKey, TValue>
{
    private Dictionary<TKey, TValue> map = new Dictionary<TKey, TValue>(); //正在追踪的图片的集合，图片名字--图片game object

    public int count
    {
        get { return map.Count; }
    }

    /// <summary>
    /// 向map添加元素
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void set(TKey key, TValue value)
    {
        try
        {
            map.Add(key, value);
        }
        catch (ArgumentException)
        {
            map.Remove(key);
            map.Add(key, value);
        }
    }


    /// <summary>
    /// 通过key获取map
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public TValue get(TKey key)
    {
        TValue value;
        map.TryGetValue(key, out value);
        return value;
    }

    /// <summary>
    /// 通过key移除map
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool remove(TKey key)
    {
        return map.Remove(key);
    }

    /// <summary>
    /// 清空map
    /// </summary>
    /// <returns></returns>
    public bool removeAll()
    {
        map.Clear();
        return true;
    }
}