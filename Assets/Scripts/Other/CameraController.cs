using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform cameraTrans;
    public float standardSpeed;
    public float zoomSpeed;

    private void Awake()
    {
        cameraTrans = transform;
    }

    private void Update()
    {
        // 相机缩放
        float m = -Input.GetAxis("Mouse ScrollWheel");
        cameraTrans.Translate(new Vector3(0, m * zoomSpeed, 0) * Time.deltaTime, Space.World);
        
        // 相机移动
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 v3 = new Vector3(h, 0, v);
        v3 *= Input.GetKey(KeyCode.LeftControl) ? (standardSpeed * 2) : standardSpeed;
        cameraTrans.Translate(v3 * Time.deltaTime, Space.World);
    }
}
