using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnitBeDamage_Inf_Hp : MonoBehaviour, IBeDamage
{
    [SerializeField] int totaldamage;

    private void Start()
    {
        Init();
    }

    public void Die()
    {
        Debug.Log("heros never die!");
    }

    public void Init()
    {
        totaldamage = 0;
    }

    public void BeDamage(int damage, IBullet frombullet, UnitController fromunit)
    {
        totaldamage += damage;
        Debug.Log("被攻击了: " + damage.ToString() + " damage, 总收到伤害: " + totaldamage);
    }
}
