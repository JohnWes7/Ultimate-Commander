using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualBeDamage : MonoBehaviour, IBeDamage
{
    public void BeDamage(int damage)
    {
        UnitController unit;
        if (TryGetComponent<UnitController>(out unit))
        {
            unit.SetHP(unit.GetHP() - damage);
        }
    }
}