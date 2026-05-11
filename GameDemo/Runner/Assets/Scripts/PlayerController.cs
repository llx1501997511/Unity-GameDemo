using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float jumpForce = 10f;      // 跳跃力度（向上初速度）
    private Rigidbody2D rb;           // 2D刚体组件（物理控制）
    private bool isGrounded;          // 是否站在地面上（防止空中二段跳）
    private bool isGameOver = false;   // 游戏是否结束（结束时不响应输入）

    void Start()
    {
        // 获取刚体组件（挂载在同一个物体上）
        rb = GetComponent<Rigidbody2D>();  // 获取挂在同一物体上的刚体组件
    }

    void Update()
    {
        // 游戏结束时不再响应输入（角色无法跳跃）
        if (isGameOver) return;

        // 按下空格 且 角色在地面上
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // 设置向上速度，X轴速度保持不变
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;  // 跳跃后不在空中，防止二段跳
        }
    }

    // 碰撞检测：当碰到其他物体时调用
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;   // 碰到地面，可以再次跳跃
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();          // 碰到障碍物，游戏结束
        }
    }
    void GameOver()
    {
        isGameOver = true;                    // 标记游戏结束
        rb.velocity = Vector2.zero;          // 停止移动（速度归零）
        rb.simulated = false;                // 禁用物理模拟（不再受重力影响）
        Debug.Log("Game Over!");             // 控制台输出提示
                                             // 通知GameManager游戏结束
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
            gm.EndGame();
    }
    public void RestartGame()
    {
        isGameOver = false;                   // 解除游戏结束标记
        rb.simulated = true;                 // 重新启用物理
        transform.position = new Vector3(0, -2.5f, 0);  // 重置位置
        rb.velocity = Vector2.zero;          // 重置速度
        isGrounded = true;                   // 重置地面状态
    }
    public bool IsGameOver() { return isGameOver; }
}
