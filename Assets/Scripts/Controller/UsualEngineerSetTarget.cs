using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualEngineerSetTarget : UsualSetTarget
{
    public override void SetTarget<T>(T target)
    {
        if (target == null)
        {
            return;
        }

        // 如果是unit
        if (target is UnitController)
        {
            UnitController unitController = target as UnitController;
            // 判断是否激活
            if (!unitController.gameObject.activeInHierarchy)
            {
                base.target.enabled = false;
                return;
            }
            this.target.value = unitController;
            this.target.enabled = true;
        }
    }

    protected override void UpdateTarget()
    {
        if (target.value == null || !target.value.gameObject.activeInHierarchy || target.value.GetHP() >= target.value.GetMaxHp())
        {
            Stop();
        }
    }

    public override void Stop()
    {
        base.Stop();
    }
}
