using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int coinValue = 1;  // 一个金币的值
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        // 检查碰到的是不是玩家
        if (other.CompareTag("Player"))
        {
            // 增加分数
            if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddScore(coinValue);


            // 销毁金币
            Destroy(gameObject);
        }
    }
}
