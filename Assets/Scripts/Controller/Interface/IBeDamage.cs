using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBeDamage
{
    public void BeDamage(int damage, IBullet frombullet, UnitController fromunit);

    public void Die();
}
