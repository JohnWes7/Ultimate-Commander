using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualBeDamage : MonoBehaviour, IBeDamage
{
    [SerializeField] private OptionalValue<UnitController> unit;

    private void Awake()
    {
        Init();
    }

    public void BeDamage(int damage)
    {
        unit.value.SetHP(unit.value.GetHP() - damage);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Init()
    {
        unit.value = GetComponent<UnitController>();
    }
}