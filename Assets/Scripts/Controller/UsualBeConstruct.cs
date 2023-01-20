using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualBeConstruct : MonoBehaviour, IBeConstruct
{
    [SerializeField] private int constructProgress;
    [SerializeField] private int maxConstructProgress;
    [SerializeField] private UnitInfo unitInfo;

    public void Init(UnitInfo unitInfo)
    {
        this.unitInfo = unitInfo;
    }

    public void AddConstructer(UnitController unitController)
    {
        
    }

    public void BeConstruct(int value)
    {
        constructProgress += value;
        constructProgress = Mathf.Clamp(constructProgress, 0, maxConstructProgress);
    }

    public UnitInfo GetUnitInfo()
    {
        if (unitInfo == null)
        {
            Debug.Log("UsualBeConstruct : GetUnitInfo uniinfo没有数据 是否进行了初始化");
        }

        return unitInfo;
    }

    

    public void RemoveConstructer(UnitController unitController)
    {
        
    }
}
