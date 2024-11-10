using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;  // 单例
    public GameObject backgroundPrefab; // 背景片段预设
    private Queue<GameObject> pool = new Queue<GameObject>(); // 对象池

    void Awake()
    {
        // 确保实例是唯一的
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            // 如果对象池中有可用对象，取出一个并激活
            GameObject obj = pool.Dequeue();
            obj.SetActive(true); // 激活对象
            return obj;
        }
        else
        {
            // 如果对象池中没有对象，则实例化一个新对象
            GameObject newObj = Instantiate(backgroundPrefab);
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        // 将对象放回池中并禁用
        obj.SetActive(false); // 隐藏对象
        pool.Enqueue(obj);
    }
}
