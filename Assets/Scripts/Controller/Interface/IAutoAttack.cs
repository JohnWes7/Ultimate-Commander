using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAutoAttack
{
    // 现在这个还没有什么用首要目标是直接获取的 tryattack里面的目标
    public void SetFirstTarget(UnitController unit);
}
