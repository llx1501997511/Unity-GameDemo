using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float walkSpeed = 5f;
    public float mouseSensitivity = 2f;

    private float verticalRotation = 0f;

    void Start()
    {
        // 锁定鼠标到游戏窗口
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 鼠标控制视角
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 水平旋转（左右转头）
        transform.Rotate(0, mouseX, 0);

        // 垂直旋转（上下看），限制角度防止翻转
        verticalRotation -= mouseY;// 鼠标上移时mouseY为正，所以用减法
        // 限制角度在 -80 到 80 度之间（不能完全上下）
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
        // 只旋转摄像机（不旋转身体）
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // WASD移动
        // 获取输入（-1到1之间）
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // 计算移动向量
        // transform.right：物体的右方向（随身体旋转而改变）
        // transform.forward：物体的前方向（随身体旋转而改变）
        Vector3 move = (transform.right * horizontal + transform.forward * vertical) * walkSpeed * Time.deltaTime;
        transform.position += move;
    }
}
