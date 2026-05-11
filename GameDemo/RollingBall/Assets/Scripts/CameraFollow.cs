using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;        // 要跟随的目标（玩家）
    public float smoothSpeed = 0.125f;  // 跟随平滑度
    public Vector3 offset = new Vector3(0, 5, -8);  // 摄像机与玩家的偏移量

    void LateUpdate()
    {
        if (target == null) return;

        // 目标位置 = 玩家位置 + 偏移量
        Vector3 desiredPosition = target.position + offset;
        // 平滑移动
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // 应用位置
        transform.position = smoothedPosition;

        // 让摄像机一直看着玩家
        transform.LookAt(target);
    }
}
