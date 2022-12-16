using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerSetTarget : UsualSetTarget
{
    public override void SetTarget(UnitController target)
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            base.target.enabled = false;
            return;
        }

        this.target.value = target;
        this.target.enabled = true;
        //Utility.MouseToTerr
    }
}
