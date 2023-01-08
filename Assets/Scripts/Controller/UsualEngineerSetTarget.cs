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

        //if (target is IBeConstruct)
        //{
        //    IBeConstruct beConstruct = target as IBeConstruct;
        //    if (!beConstruct.GetGameObject().activeInHierarchy)
        //    {
        //        constructTarget.enabled = false;
        //    }
        //    constructTarget.value = beConstruct;
        //    constructTarget.enabled = true;
        //}
    }

    public override void Stop()
    {
        base.Stop();
        //constructTarget.enabled = false;
    }
}
