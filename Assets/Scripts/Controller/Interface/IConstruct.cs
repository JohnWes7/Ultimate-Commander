using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConstruct
{
    public void SetConstructTarget(IBeConstruct beConstruct);

    public List<string> GetIConstructList();
    public void BeConstructToFoundation(UnitController unitController);
}
