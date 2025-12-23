using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEaseFllow : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        SetPostion();
    }

    /// <summary>
    /// 设置相机位置
    /// </summary>
    void SetPostion() {
        Vector3 newPostion = new Vector3(target.position.x,target.position.y, transform.position.z);
        transform.position = newPostion;
    }
}
