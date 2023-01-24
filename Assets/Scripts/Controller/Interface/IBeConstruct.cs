using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBeConstruct
{
    public void Init(UnitInfo unitInfo);

    public void BeConstruct();

    public void AddConstructer(IConstruct unitController);

    public void RemoveConstructer(IConstruct unitController);

    public UnitInfo GetUnitInfo();

    public GameObject GetGameObject();
}
