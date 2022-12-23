using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrefabs : Single<BulletPrefabs>
{
    private GameObject tankBulletPrefab;
    public GameObject TankBulletPrefab
    {
        get
        {
            return tankBulletPrefab;
        }
    }

    public BulletPrefabs()
    {
        tankBulletPrefab = Resources.Load<GameObject>(ResourcesPath.TankBullet_PATH);
    }
    
}
