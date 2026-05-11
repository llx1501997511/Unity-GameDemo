using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update
    public float shootRange = 100f;      // 射击距离
    public GameObject hitEffect;         // 击中特效（稍后添加）

    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 射线起点：主摄像机的位置
        // 射线方向：摄像机的前方（屏幕正中央）
        // 从屏幕中心发射一条射线
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;// 存储击中信息（击中点、击中物体等）


        // 如果射线击中了物体
        // Physics.Raycast(射线, 输出击中信息, 最大距离)
        if (Physics.Raycast(ray, out hit, shootRange))
        {
            // 检查击中的物体是否有Target标签
            if (hit.collider.CompareTag("Target"))
            {


                // 击中靶子，销毁它
                //Destroy(hit.collider.gameObject);
                GameObject hitTarget = hit.collider.gameObject;

                // 通知生成器：这个靶子被击毁了
                TargetSpawner spawner = FindObjectOfType<TargetSpawner>();
                if (spawner != null)
                {
                    spawner.OnTargetDestroyed(hitTarget);
                }

                // 击中靶子，销毁它
                Destroy(hitTarget);


                // 加分
                if (ScoreManager.Instance != null)
                    ScoreManager.Instance.AddScore(10);

                // 播放击中特效
                if (hitEffect != null)
                {
                    // hit.point: 击中点的世界坐标
                    // hit.normal: 击中面的法线方向（用于让特效垂直于被击中面）
                    Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    //Destroy(hiteffect, 1f);  // 1秒后销毁特效物体,如果特效预制体没有设置销毁或者循环播放，可以代码销毁
                }

                Debug.Log("击中靶子！ +10分，剩余靶子：" + (spawner != null ? spawner.GetRemainingTargets() : 0));
            }
        }
    }
}
