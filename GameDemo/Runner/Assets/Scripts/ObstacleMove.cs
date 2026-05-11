using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 3f;        // 移动速度
    public float destroyX = -8f;        // 超出这个X坐标就销毁（左侧）

    void Update()
    {
        // 每帧向左移动
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // 超出屏幕左侧就返回对象池
        if (transform.position.x < destroyX)
        {
            // 找到对象池并归还物体
            ObjectPool pool = FindObjectOfType<ObjectPool>();
            if (pool != null)
                pool.ReturnObject(gameObject);
            else
                Destroy(gameObject);  // 备用：直接销毁
        }
    }
}
