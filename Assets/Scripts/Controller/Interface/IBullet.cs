using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    public void SetTarget(UnitController unit, UnitController from, int damage, float speed);
}
