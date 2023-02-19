using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretBase : MonoBehaviour, IAutoAttack
{
    // warshipster
    [SerializeField]
    private OptionalValue<UnitController> firstTarget
    {
        get
        {
            try
            {
                return m_tryAttack.GetTarget();
            }
            catch (System.Exception)
            {
                OptionalValue<UnitController> temp = new OptionalValue<UnitController>(null);
                temp.enabled = false;
                return temp;
            }
        }
    }
    [SerializeField] private OptionalValue<UnitController> secondTarget;
    [SerializeField] private OptionalValue<UnitController> attackTarget;

    [SerializeField] private float turretRange; // 炮塔射程
    [SerializeField] private int bulletDamage;  // 子弹伤害
    // 留白 炮塔类型 比如只能对地只能对空只能对什么
    [SerializeField] private UnitController m_UnitController;   //本身的controller
    [SerializeField] private ISetTarget m_tryAttack;    // 获取当前首要目标
}
