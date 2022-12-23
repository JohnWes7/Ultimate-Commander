using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabs : Single<BulletPrefabs>
{
    public GameObject FactoryPrefab { get; }

    public UnitPrefabs()
    {
        FactoryPrefab = Resources.Load<GameObject>(ResourcesPath.Factory_PATH);
    }
}
