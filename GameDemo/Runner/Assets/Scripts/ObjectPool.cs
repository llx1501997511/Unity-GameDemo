using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obstaclePrefab;   // 障碍物预制体
    public int poolSize = 5;             // 池子大小

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // 预先创建 poolSize 个障碍物，全部禁用
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // 从池子里取出一个障碍物
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // 池子空了就新建一个（备用）
            GameObject obj = Instantiate(obstaclePrefab);
            return obj;
        }
    }

    // 把障碍物放回池子
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
