using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualConstruct : MonoBehaviour, IConstruct, IStop
{
    [SerializeField] private List<string> ConstructNameList;
    [SerializeField] private OptionalValue<IBeConstruct> beConstruct;
    [SerializeField] private float range
    {
        get
        {
            ISetTarget setTarget = GetComponent<ISetTarget>();
            if (setTarget != null)
            {
                return setTarget.GetRange();    
            }

            return -1f;
        }
    }

    private void Update()
    {
        TryConstructTarget();
    }

    private void TryConstructTarget()
    {
        // 如果有下达指令的目标 监测是否在攻击距离里面
        if (beConstruct.enabled)
        {
            // 超出范围移动
            if ((beConstruct.value.GetGameObject().transform.position - transform.position).magnitude > range)
            {
                IMove move;
                if (TryGetComponent<IMove>(out move))
                {
                    move.SetMoveDest(beConstruct.value.GetGameObject().transform.position + ((transform.position - beConstruct.value.GetGameObject().transform.position).normalized * range * 0.95f));
                }
            }
            else
            {
                beConstruct.value.BeConstruct();
                beConstruct.enabled = false;
            }
        }
    }

    public void SetConstructTarget(IBeConstruct beConstruct)
    {
        Debug.Log("UsualConstruct : SetConstructTarget 接受建造指令 " + beConstruct.GetUnitInfo().Name);
        this.beConstruct.value = beConstruct;
        this.beConstruct.enabled = true;
        beConstruct.AddConstructer(this);
    }

    public List<string> GetIConstructList()
    {
        return ConstructNameList;
    }

    private void CancelConstruct()
    {
        // 报告取消建造
        if (beConstruct.enabled)
        {
            beConstruct.value.RemoveConstructer(this);
        }
    }

    public void Stop()
    {
        // 报告取消回调
        CancelConstruct();
        this.beConstruct.enabled = false;
    }

    private void OnDestroy()
    {
        CancelConstruct();   
    }

    private void OnDisable()
    {
        CancelConstruct();
    }

    /// <summary>
    /// 基址建成回调
    /// </summary>
    /// <param name="unit"></param>
    public void BeConstructToFoundation(UnitController unit)
    {
        beConstruct.enabled = false;
        ISetTarget setTarget;
        if (unit.TryGetComponent<ISetTarget>(out setTarget))
        {
            Debug.Log("UsualConstruct : BeConstructToFoundation 完成虚影模型 接受模型指令: " + unit.name + " 位置: " + unit.transform.position);
            setTarget.SetTarget<UnitController>(unit);
        }
    }
}
