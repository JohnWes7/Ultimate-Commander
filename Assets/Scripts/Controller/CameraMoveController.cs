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
            Vector3 endmousePos = Input.mousePosition;
            Vector3 deltaVect = endmousePos - startMousePos;
            startMousePos = endmousePos;

            dest = dest + new Vector3(deltaVect.x, 0, deltaVect.y) * Config.CameraMoveSpeed * Mathf.Abs(Camera.main.transform.position.y);
            transform.DOMove(dest, 0.8f).SetEase(Ease.OutExpo);
        }
    }

    public void SetDest(Vector3 dest)
    {
        this.dest = dest;
    }
}
