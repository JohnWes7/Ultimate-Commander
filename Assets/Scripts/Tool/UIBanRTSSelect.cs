using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 让鼠标放上去时可以不进行单位选择 禁用掉rtscontroller的update 以及rtsconstruct
/// </summary>
public class UIBanRTSSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 用 enable 禁用 rts update
        BanRTS();

        // 禁用 建造
        BanConstruct();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        // 用 enable 禁用 rts update
        BanRTS();

        // 禁用 建造
        BanConstruct();
    }

    /// <summary>
    /// 鼠标移出时启用rts控制器和建造
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //重新启用
        EnableRTS();
        EnableConstruct();
    }

    private void BanRTS()
    {
        if (!GameRTSController.Instance.IsSelecting())
        {
            GameRTSController.Instance.enabled = false;
        }
    }

    private void BanConstruct()
    {
        GameRTSConstructController.Instance.SetCanConstruct(false);
    }

    private void EnableRTS()
    {
        GameRTSController.Instance.enabled = true;
    }

    private void EnableConstruct()
    {
        GameRTSConstructController.Instance.SetCanConstruct(true);
    }
}
