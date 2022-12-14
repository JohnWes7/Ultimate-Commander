using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : Single<BulletData>
{
    private GameObject tankBulletPrefab;
    public GameObject TankBulletPrefab
    {
        get
        {
            return tankBulletPrefab;
        }
    }

    public BulletData()
    {
        tankBulletPrefab = Resources.Load<GameObject>(ResourcesPath.TankBullet_PATH);
    }
    
}
