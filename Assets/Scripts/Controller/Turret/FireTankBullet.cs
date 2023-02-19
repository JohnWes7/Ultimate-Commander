using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTankBullet : MonoBehaviour, IFire
{
    [SerializeField] private OptionalValue<Transform> FirePos;

    private void Start()
    {
        Transform pos = FindBulletFirePos(this.transform);
        if (pos != null)
        {
            FirePos.value = pos;
            FirePos.enabled = true;
        }
    }

    public static Transform FindBulletFirePos(Transform parent)
    {
        Transform pos = parent.Find("BulletFirePos");
        if (pos == null)
        {
            return parent;
        }
        return pos;
    }


    public void Fire(UnitController target, UnitController firefrom, int damage, params object[] values)
    {
        Vector3 instPos = transform.position;
        Quaternion instQua = transform.rotation;
        if (FirePos.enabled)
        {
            instPos = FirePos.value.position;
            instQua = FirePos.value.rotation;
        }

        IBullet temp = Instantiate<GameObject>(BulletPrefabs.Instance.TankBulletPrefab, FirePos.value.position, FirePos.value.rotation).GetComponent<IBullet>();
        temp.SetTarget(target, firefrom, damage, (float)values[0]);
    }
}
