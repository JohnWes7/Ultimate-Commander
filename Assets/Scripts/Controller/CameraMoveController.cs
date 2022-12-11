using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 负责摄像机移动
/// </summary>
public class CameraMoveController : MonoBehaviour
{
    // 相机位置
    private Vector3 dest;
    // 中键拖动用的起始位置
    private Vector3 startMousePos;

    private void Awake()
    {
        dest = transform.position;
    }

    private void Update()
    {
        // 滚轮控制
        float mousescroll = Input.GetAxis("Mouse ScrollWheel");
        if (mousescroll < 0)
        {
            dest = dest + Vector3.down * Config.CameraZoomSpeed * mousescroll;
            transform.DOMove(dest, 0.8f).SetEase(Ease.OutExpo);
        }
        else if(mousescroll > 0)
        {
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward) - Camera.main.transform.position;
            dir = dir.normalized;
            dest = dest + dir * Config.CameraZoomSpeed * mousescroll;
            transform.DOMove(dest, 0.8f).SetEase(Ease.OutExpo);
        }

        // 中键控制
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            startMousePos = Input.mousePosition;
        }
        if (Input.GetKey(KeyCode.Mouse2))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            //Debug.Log(mouseX + " " + mouseY);

            if (mouseX != 0 || mouseY != 0)
            {
                dest = dest + new Vector3(-mouseX, 0, -mouseY) * Config.CameraMoveSpeed * Mathf.Abs(Camera.main.transform.position.y) * 0.01f;
                transform.DOMove(dest, 0.3f).SetEase(Ease.OutExpo);
            }
        }
    }

    public void SetDest(Vector3 dest)
    {
        this.dest = dest;
    }
}
