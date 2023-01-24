using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualSetTarget : MonoBehaviour, ISetTarget
{
    [SerializeField] protected OptionalValue<UnitController> target;
    // 表面射程范围 超过这个范围就会移动
    [SerializeField] protected float range;

    public float GetRange()
    {
        return range;
    }

    public OptionalValue<UnitController> GetTarget()
    {
        return target;
    }

    /// <summary>
    /// 设置要攻击的目标
    /// </summary>
    /// <param name="target"></param>
    public virtual void SetTarget<T>(T target)
    {
        if (!(target is UnitController) || target == null)
        {
            return;
        }

        UnitController unitarget = target as UnitController;
        if (!unitarget.gameObject.activeInHierarchy)
        {
            this.target.enabled = false;
            return;
        }

        // 不能是友军
        if (unitarget.GetTeam() == GetComponent<UnitController>().GetTeam())
        {
            this.target.enabled = false;
            return;
        }

        // 必须要能被攻击
        IBeDamage beDamage;
        if (!unitarget.TryGetComponent<IBeDamage>(out beDamage))
        {
            this.target.enabled = false;
            return;
        }

        this.target.value = unitarget;
        this.target.enabled = true;
    }

    public virtual void Stop()
    {
        target.enabled = false;
    }

    protected virtual void UpdateTarget()
    {
        if (target.value == null || !target.value.gameObject.activeInHierarchy)
        {
            target.enabled = false;
        }
    }

    private void Update()
    {
        UpdateTarget();

        // 如果有下达指令的目标 监测是否在攻击距离里面
        if (target.enabled)
        {
            // 超出范围移动
            if ((target.value.transform.position - transform.position).magnitude > range)
            {
                IMove move;
                if (TryGetComponent<IMove>(out move))
                {
                    move.SetMoveDest(target.value.transform.position + ((transform.position - target.value.transform.position).normalized * range * 0.8f));
                }
            }
        }
    }
}
