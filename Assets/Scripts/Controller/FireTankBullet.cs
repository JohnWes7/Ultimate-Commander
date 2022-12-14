using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTankBullet : MonoBehaviour, IFire
{
    private Vector3 FirePos;

    private void Start()
    {
        Transform pos = transform.Find("BulletPos");
        if (pos != null)
        {
            FirePos = pos.position - transform.position;
        }
    }


    public void Fire()
    {
        Instantiate<GameObject>(BulletData.Instance.TankBulletPrefab, transform.position + FirePos, Quaternion.identity);
    }
}
