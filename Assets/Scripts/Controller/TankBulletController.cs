using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBulletController : MonoBehaviour, IBullet
{
    [SerializeField] private int team;
    [SerializeField] private OptionalValue<Vector3> dir;
    [SerializeField] private UnitController fromUnit = null;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float timer;
    [SerializeField] private float maxRetainTime;
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 size;

    public void SetTarget(UnitController unit, UnitController from, int damage, float speed)
    {
        dir.value = (unit.transform.position - transform.position).normalized;
        dir.enabled = true;
        team = from.GetTeam();
        this.damage = damage;
        this.speed = speed;
        this.fromUnit = from;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxRetainTime)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        
        if (dir.enabled)
        {
            // 移动
            transform.position += dir.value * Time.fixedDeltaTime * speed;

            // 接触检测 如果打到敌人
            Collider[] colliders = Physics.OverlapBox(transform.position + center, size/2, Quaternion.identity, 1 << LayerMask.NameToLayer("Unit"));
            foreach (var item in colliders)
            {
                UnitController unit;
                if (item.TryGetComponent<UnitController>(out unit))
                {
                    if (unit.GetTeam() != this.team)
                    {
                        IBeDamage beDamage;
                        if (unit.TryGetComponent<IBeDamage>(out beDamage))
                        {
                            beDamage.BeDamage(damage, this, fromUnit);
                            Die();
                        }
                    }
                }
            }

            // 如果打到地面
            colliders = Physics.OverlapBox(transform.position + center, size / 2, Quaternion.identity, 1 << LayerMask.NameToLayer("Ground"));
            if (colliders.Length > 0)
            {
                Die();
            }
        }

        
    }
}