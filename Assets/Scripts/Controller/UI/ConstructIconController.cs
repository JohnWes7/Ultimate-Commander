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

    /// <summary>
    /// 初始化显示
    /// </summary>
    /// <param name="unitInfo"></param>
    public void Init(UnitInfo unitInfo)
    {
        this.unitInfo = unitInfo;

        // 更改图标
        if (this.unitInfo.sprite)
        {
            image.sprite = unitInfo.sprite;
        }

        text.text = this.unitInfo.Name;
    }
}
