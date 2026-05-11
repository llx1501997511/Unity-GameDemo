using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("生成参数")]
    public GameObject targetPrefab;      // 靶子预制体（需要拖拽赋值）
    public int targetCount = 20;         // 要生成的靶子总数

    // 生成区域的范围（最小点 → 最大点，形成一个矩形区域）
    public Vector3 spawnAreaMin = new Vector3(-5f, 2f, -6f);
    public Vector3 spawnAreaMax = new Vector3(5f, 2f, -2f);

    [Header("旋转设置")]
    public bool facePlayer = true;       // 是否让靶子面向玩家

    private List<GameObject> activeTargets = new List<GameObject>();  // 存储当前存活的靶子
    private int targetsDestroyed = 0;     // 已击毁的靶子数量（计数器）

    void Start()
    {
        GenerateAllTargets();// 游戏开始时生成所有靶子
    }

    void GenerateAllTargets()
    {
        for (int i = 0; i < targetCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject newTarget = Instantiate(targetPrefab, randomPosition, Quaternion.identity);

            // 让靶子面向玩家（假设玩家在 Z=5 左右的位置）
            if (facePlayer)
            {
                // LookAt 会同时旋转 X/Y/Z 轴，可能导致靶子倒下
                newTarget.transform.LookAt(new Vector3(0, 0.5f, 5f));
                // 结果：靶子可能变这样 → ／￣＼ 倒下的靶子

                // 所以只保留Y轴旋转（左右转），X和Z轴归零
                newTarget.transform.eulerAngles = new Vector3(90, newTarget.transform.eulerAngles.y, 0);
                // 结果：靶子始终站立，只水平旋转
            }

            activeTargets.Add(newTarget);
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        float z = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
        return new Vector3(x, y, z);
    }

    // 当靶子被击中时调用此方法
    public void OnTargetDestroyed(GameObject destroyedTarget)
    {
        if (activeTargets.Contains(destroyedTarget))// 确认这个靶子在列表中
        {
            activeTargets.Remove(destroyedTarget);// 从列表移除
            targetsDestroyed++;// 计数器+1

            // 检查是否所有靶子都被打完了
            if (activeTargets.Count == 0)
            {
                Debug.Log("所有靶子已击毁！游戏结束");
                // 通知 GameManager 游戏胜利结束
                ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
                if (scoreManager != null)
                {
                    scoreManager.GameWin();  // 后面会添加这个方法
                }
            }
        }
    }

    // 获取剩余靶子数量（用于UI显示）
    public int GetRemainingTargets()
    {
        return activeTargets.Count;
    }

    // 重置游戏时重新生成靶子
    public void ResetAndRespawn()
    {
        // 销毁所有现有靶子
        foreach (GameObject target in activeTargets)
        {
            if (target != null)
                Destroy(target);
        }
        activeTargets.Clear();
        targetsDestroyed = 0;

        // 重新生成
        GenerateAllTargets();
    }
}
