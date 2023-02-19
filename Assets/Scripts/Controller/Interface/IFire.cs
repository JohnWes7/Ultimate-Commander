using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFire
{
    public void Fire(UnitController unit, UnitController from, int damage, params object[] values);
}
