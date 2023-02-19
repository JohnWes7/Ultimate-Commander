using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualBeRepaire : MonoBehaviour, IBeRepaired
{
    [SerializeField] private UnitController unitController;

    private void Start()
    {
        unitController = GetComponent<UnitController>();
    }

    public void IBeRepaired(int value)
    {
        int hp = unitController.GetHP();
        hp += value;
        unitController.SetHP(Mathf.Clamp(hp, 0, unitController.GetMaxHp()));
    }
}
