using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove : IStop
{
    public void SetMoveDest(Vector3 dest);
}
