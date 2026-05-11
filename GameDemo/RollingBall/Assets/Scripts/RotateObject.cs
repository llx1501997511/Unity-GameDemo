using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotateSpeed = 100f;  // 旋转速度

    void Update()
    {
        // 绕着Y轴旋转
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
