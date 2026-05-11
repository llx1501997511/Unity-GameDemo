using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winPanel;
    public TextMeshProUGUI finalScoreText;

    private BallController ballController;
    private bool isGameActive = true;

    void Start()
    {
        // 获取玩家控制器
        ballController = FindObjectOfType<BallController>();

        // 确保胜利面板隐藏
        if (winPanel != null)
            winPanel.SetActive(false);

        // 锁定鼠标（可选）
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void WinGame()
    {
        if (!isGameActive) return;

        isGameActive = false;

        // 显示胜利面板
        if (winPanel != null)
        {
            winPanel.SetActive(true);

            // 更新最终得分显示
            if (finalScoreText != null && ScoreManager.Instance != null)
            {
                finalScoreText.text = "Fianl Score: " + ScoreManager.Instance.GetCurrentScore() +
                                      " / " + ScoreManager.Instance.GetTotalCoins();
            }
        }

        // 禁用玩家控制
        if (ballController != null)
            ballController.enabled = false;

        // 释放鼠标
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        // 重新加载当前场景
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}
