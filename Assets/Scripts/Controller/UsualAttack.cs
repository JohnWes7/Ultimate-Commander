using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualAttack : MonoBehaviour, ITryAttack
{
    [SerializeField] private UnitController target;
    // 表面射程范围 超过这个范围就会移动
    [SerializeField] private float range;

    /// <summary>
    /// 设置要攻击的目标
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(UnitController target)
    {
        if (target == null)
        {
            this.target = target;
            return;
        }

        // 不能是友军
        if (target.GetTeam() == GetComponent<UnitController>().GetTeam())
        {
            target = null;
            return;
        }

        // 必须要能被攻击
        IBeDamage beDamage;
        if (!target.TryGetComponent<IBeDamage>(out beDamage))
        {
            target = null;
            return;
        }

        this.target = target;
    }

    public void Stop()
    {
        SetTarget(null);
    }

    private void Update()
    {
        // 如果有下达指令的目标 监测是否在攻击距离里面
        if (target != null)
        {
            // 超出范围移动
            if ((target.transform.position - transform.position).magnitude > range)
            {
                IMove move;
                if (TryGetComponent<IMove>(out move))
                {
                    move.SetMoveDest(target.transform.position + ((transform.position - target.transform.position).normalized * range * 0.8f));
                }
            }
            
        }
    }
}
