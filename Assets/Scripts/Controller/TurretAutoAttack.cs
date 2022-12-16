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

    [SerializeField] private float turretRange; // 炮塔射程
    [SerializeField] private float turretcd;    // 炮塔射速
    [SerializeField] private int bulletDamage;  // 子弹伤害
    [SerializeField] private float speed;   // 子弹速度
    // 留白 炮塔类型 比如只能对地只能对空只能对什么
    [SerializeField] private UnitController m_UnitController;   //本身的controller
    [SerializeField] private ISetTarget m_tryAttack;    // 获取当前首要目标
    [SerializeField] private OptionalValue<IFire> m_fire;   // 开火函数
    [SerializeField] private float timer;

    private void Awake()
    {
        Init();
    }

    private void Update()
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

    private void TryFire()
    {
        // 可能有的一帧攻击好几次
        timer = timer + Time.deltaTime * turretcd;
        //Debug.Log(m_fire.enabled.ToString() + " " + attackTarget.enabled.ToString() + " " + (timer >= 1).ToString() + " " + (Vector3.Angle(transform.forward, attackTarget.value.transform.position - transform.position) < 10));
        if (m_fire.enabled && attackTarget.enabled && timer >= 1 && Vector3.Angle(transform.forward, attackTarget.value.transform.position - transform.position) < 5)
        {
            timer -= 1;
            m_fire.value.Fire(attackTarget.value, m_UnitController, bulletDamage, speed);
        }
        timer = Mathf.Clamp01(timer);
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

    /// <summary>
    /// 弃用
    /// </summary>
    /// <param name="unit"></param>
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

}
