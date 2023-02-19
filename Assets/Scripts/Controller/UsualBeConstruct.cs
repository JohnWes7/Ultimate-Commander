using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualBeConstruct : MonoBehaviour, IBeConstruct
{
    [SerializeField] private int constructProgress;
    [SerializeField] private int maxConstructProgress;
    [SerializeField] private UnitInfo unitInfo;
    [SerializeField] private List<IConstruct> constructUnits = new List<IConstruct>();
    [SerializeField] private string player;
    [SerializeField] private int team;
    [SerializeField] private GameObject foundation;

    public void Init(UnitInfo unitInfo, int team, string player)
    {
        this.unitInfo = unitInfo;
        this.team = team;
        this.player = player;
    }

    public void AddConstructer(IConstruct unitController)
    {
        constructUnits.Add(unitController);
    }

    /// <summary>
    /// 建立基址 -> repire函数修好 -> fallback调用工厂运行
    /// </summary>
    public void BeConstruct()
    {
        UnitController foundation = Instantiate<GameObject>(unitInfo.Prefab, transform.position, Quaternion.identity).GetComponent<UnitController>();
        //初始化
        foundation.Init(team, player, unitInfo.Name, GameRTSController.Instance.GetTeamColor(), 0, unitInfo.MaxHp);

        // 地基回调
        foreach (IConstruct item in constructUnits)
        {
            item.BeConstructToFoundation(foundation);
        }
        // 销毁
        Destroy(gameObject);
    }

    public UnitInfo GetUnitInfo()
    {
        if (unitInfo == null)
        {
            Debug.Log("UsualBeConstruct : GetUnitInfo uniinfo没有数据 是否进行了初始化");
        }
        return unitInfo;
    }

    // 取消建造时候要 判断虚影
    public void RemoveConstructer(IConstruct unitController)
    {
        if (unitController == null)
        {
            return;
        }

        //如果没有该建造器 报错提醒
        if (!constructUnits.Contains(unitController))
        {
            Debug.LogWarning("UsualBeConstruct : RemoveConstructer 不存在该建造者 " + (unitController as MonoBehaviour).name);
        }
        constructUnits.Remove(unitController);

        // 如果没人建造了 销毁自己
        if (constructUnits.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
