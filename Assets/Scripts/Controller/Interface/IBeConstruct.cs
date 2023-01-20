using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBeConstruct
{
    public void Init(UnitInfo unitInfo);

    public void BeConstruct(int value);

    public void AddConstructer(UnitController unitController);

    public void RemoveConstructer(UnitController unitController);

    public UnitInfo GetUnitInfo();
}
