using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructIconController : MonoBehaviour
{
    [SerializeField] private UnitInfo unitInfo;
    [SerializeField] private Text text;
    [SerializeField] private Image image;

    /// <summary>
    /// 初始化显示
    /// </summary>
    /// <param name="unitInfo"></param>
    public void Init(UnitInfo unitInfo)
    {
        this.unitInfo = unitInfo;
        if (this.unitInfo.sprite)
        {

        }

        text.text = this.unitInfo.Name;
    }
}
