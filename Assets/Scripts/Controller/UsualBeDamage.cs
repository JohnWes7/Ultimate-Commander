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

    public void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void Init()
    {
        unit.value = GetComponent<UnitController>();
    }

    public void BeDamage(int damage, IBullet frombullet, UnitController fromunit)
    {
        Debug.Log(damage);
        unit.value.SetHP(unit.value.GetHP() - damage);
        if (unit.value.GetHP() <= 0)
        {
            Die();
        }
    }
}