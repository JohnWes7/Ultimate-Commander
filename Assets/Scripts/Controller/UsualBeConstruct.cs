using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualBeConstruct : MonoBehaviour, IBeConstruct
{
    [SerializeField] private int constructProgress;
    [SerializeField] private int maxConstructProgress;

    public void AddConstructer(UnitController unitController)
    {
        throw new System.NotImplementedException();
    }

    public void BeConstruct(int value)
    {
        constructProgress += value;
        constructProgress = Mathf.Clamp(constructProgress, 0, maxConstructProgress);
    }

    public void RemoveConstructer(UnitController unitController)
    {
        throw new System.NotImplementedException();
    }
}
