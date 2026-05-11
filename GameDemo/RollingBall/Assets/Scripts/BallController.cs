using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 10f;      // 移动速度

    private Rigidbody rb;

    void Start()
    {
        // 获取刚体组件
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 获取输入（A/D 或 左右箭头）
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 计算移动方向
        Vector3 movement = new Vector3(horizontal, 0, vertical);

        // 给刚体施加力
        rb.AddForce(movement * moveSpeed);
    }
}
