using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAutoAttack : IStop
{
    public void SetFirstTarget(UnitController unit);
}
