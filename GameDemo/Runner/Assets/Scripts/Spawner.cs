using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public ObjectPool objectPool;   // 引用对象池
    public float spawnInterval = 1.5f;  // 生成间隔（秒）

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacle = objectPool.GetObject();
        // 生成位置：屏幕右侧外面，与玩家同高度
        obstacle.transform.position = new Vector2(8f, -2.5f);
    }
    public void ResetTimer()
    {
        timer = 0f;
    }
}
