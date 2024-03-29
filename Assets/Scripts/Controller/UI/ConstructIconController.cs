using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructIconController : MonoBehaviour
{
    [SerializeField] private UnitInfo unitInfo;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    /// <summary>
    /// 初始化显示
    /// </summary>
    /// <param name="unitInfo"></param>
    public void Init(UnitInfo unitInfo)
    {
        this.unitInfo = unitInfo;

        // 更改图标
        if (this.unitInfo.Sprite)
        {
            image.sprite = unitInfo.Sprite;
        }

        text.text = this.unitInfo.Name;
    }

    public void ButtonFallback()
    {
        Debug.Log("ConstructIconController : ButtonFallback 建造按钮回调 " + unitInfo.Name);
        GameRTSConstructController.Instance.SetConstructUnit(unitInfo);
    }
}
