using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretAutoAttack : MonoBehaviour, IAutoAttack
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

    [SerializeField] private float turretRange; //炮塔射程
    [SerializeField] private float turretcd;
    [SerializeField] private UnitController m_UnitController;
    [SerializeField] private ITryAttack m_tryAttack;

    private void Awake()
    {
        m_UnitController = GetComponentInParent<UnitController>();
        m_tryAttack = GetComponentInParent<ITryAttack>();
    }

    private void Update()
    {
        // 超出射程就取消攻击目标 attacktarget
        ResetAttackTargetIfOutOfRange();

        // 扫描目标
        ScanTarget();

        // 转动攻击
        RotateTurretAndFire();
        
    }

    private void RotateTurretAndFire()
    {
        if (attackTarget.enabled)
        {
            // 进行攻击 转动炮塔
            Vector3 dir = attackTarget.value.transform.position - transform.position;
            Quaternion dirQua = Quaternion.LookRotation(dir, Vector3.up);
            transform.DOKill();
            transform.DORotateQuaternion(dirQua, 0.3f).SetEase(Ease.Linear);
        }
        else
        {
            // 复位
            transform.DOKill();
            transform.DOLocalRotateQuaternion(Quaternion.identity, 0.2f).SetEase(Ease.Linear);
        }
    }

    private void ScanTarget()
    {
        // 寻找周围目标
        Collider[] allunits = Physics.OverlapSphere(transform.position, turretRange, 1 << LayerMask.NameToLayer("Unit"));
        //Debug.Log(allunits.Length);
        //Debug.Log(allunits);

        // 先找首要指令的目标在不在: 如果有第一目标 而且当前目标不存在 或者不是首要目标就要进行监测
        if (firstTarget.enabled && (attackTarget.enabled != true || attackTarget.value != firstTarget.value))
        {
            foreach (var item in allunits)
            {
                UnitController unitController;
                if (item.gameObject.TryGetComponent<UnitController>(out unitController))
                {
                    if (unitController == firstTarget.value)
                    {
                        attackTarget.value = unitController;
                        attackTarget.enabled = true;
                        break;
                    }
                }
            }
        }

        // 如果当前没有目标
        if (!attackTarget.enabled)
        {
            foreach (var item in allunits)
            {
                UnitController unitController;
                if (item.transform.gameObject.TryGetComponent<UnitController>(out unitController))
                {
                    if (unitController.GetTeam() != m_UnitController.GetTeam())
                    {
                        this.secondTarget.value = unitController;
                        this.secondTarget.enabled = true;
                        attackTarget.value = secondTarget.value;
                        attackTarget.enabled = true;
                        break;
                    }
                }
            }
        }
    }

    private void ResetAttackTargetIfOutOfRange()
    {
        if (attackTarget.enabled)
        {
            // 如果超过射程就target失效
            if ((attackTarget.value.transform.position - transform.position).magnitude > turretRange)
            {
                //Debug.Log((attackTarget.value.transform.position - transform.position).magnitude);
                attackTarget.enabled = false;
            }
        }
    }

    public void SetFirstTarget(UnitController unit)
    {
        //if (unit == null)
        //{
        //    firstTarget.value = null;
        //    firstTarget.enabled = false;
        //    return;
        //}

        //firstTarget.value = unit;
        //firstTarget.enabled = true;
    }

    public void Stop()
    {
        //firstTarget.enabled = false;
    }

}
