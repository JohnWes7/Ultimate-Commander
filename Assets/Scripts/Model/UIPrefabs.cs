using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrefabs : Single<UIPrefabs>
{
    public GameObject HPBarPrefab{ get; }

    public UIPrefabs()
    {
        HPBarPrefab = Resources.Load<GameObject>(ResourcesPath.HpbarPrefab_PATH);
    }
}
