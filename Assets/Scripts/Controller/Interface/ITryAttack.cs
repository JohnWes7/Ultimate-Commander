using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITryAttack : IStop
{
    // 攻击 在范围内 直接攻击, 不在范围内就走到范围再攻击
    // 反击 可以在行进路上和待机的时候 搜索目标攻击
    public void SetTarget(UnitController target);
    public OptionalValue<UnitController> GetTarget();
}
