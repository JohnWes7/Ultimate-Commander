using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretRepire : MonoBehaviour
{
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
    [SerializeField] private OptionalValue<IFire> m_fire;   // 开火函数

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        m_UnitController = GetComponentInParent<UnitController>();
        m_tryAttack = GetComponentInParent<ISetTarget>();

        IFire fire;
        if (TryGetComponent<IFire>(out fire))
        {
            m_fire.value = fire;
            m_fire.enabled = true;
        }
        else
        {
            m_fire.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 超出射程就取消攻击目标 attacktarget
        ResetAttackTargetIfOutOfRangeAndDie();

        // 扫描目标
        ScanTarget();

        // 转动炮塔
        RotateTurret();

        // 开火
        TryFire();
    }

    private void RotateTurret()
    {
        if (attackTarget.enabled)
        {
            // 进行攻击 转动炮塔
            Vector3 dir = attackTarget.value.transform.position - transform.position;
            Quaternion dirQua = Quaternion.LookRotation(dir, Vector3.up);
            transform.DOKill();
            transform.DORotateQuaternion(dirQua, 0.1f).SetEase(Ease.Linear);
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
                    if (unitController.GetTeam() == m_UnitController.GetTeam() && unitController.GetHP() < unitController.GetMaxHp())
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

    private void TryFire()
    {
        // 激光类
        if (m_fire.enabled && attackTarget.enabled)
        {
            m_fire.value.Fire(attackTarget.value, m_UnitController, bulletDamage);
        }
    }

    private void ResetAttackTargetIfOutOfRangeAndDie()
    {
        if (attackTarget.enabled)
        {
            // 如果超过射程就target失效
            if (attackTarget.value == null || (attackTarget.value.transform.position - transform.position).magnitude > turretRange)
            {
                //Debug.Log((attackTarget.value.transform.position - transform.position).magnitude);
                attackTarget.enabled = false;
            }
        }
    }
}
