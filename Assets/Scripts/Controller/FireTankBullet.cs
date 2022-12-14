using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTankBullet : MonoBehaviour, IFire
{
    [SerializeField] private OptionalValue<Transform> FirePos;

    private void Start()
    {
        Transform pos = transform.Find("BulletFirePos");
        if (pos != null)
        {
            FirePos.value = pos;
            FirePos.enabled = true;
        }
    }


    public void Fire(UnitController target, UnitController firefrom, int damage, float speed)
    {
        Vector3 instPos = transform.position;
        Quaternion instQua = transform.rotation;
        if (FirePos.enabled)
        {
            instPos = FirePos.value.position;
            instQua = FirePos.value.rotation;
        }

        IBullet temp = Instantiate<GameObject>(BulletData.Instance.TankBulletPrefab, FirePos.value.position, FirePos.value.rotation).GetComponent<IBullet>();
        temp.SetTarget(target, firefrom, damage, speed);
    }
}
