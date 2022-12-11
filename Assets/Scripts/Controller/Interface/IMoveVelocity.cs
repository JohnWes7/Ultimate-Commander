using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveVelocity : IStop
{
    public void SetVelocity(Vector3 velocity);
}
