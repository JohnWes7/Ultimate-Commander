using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualMove : MonoBehaviour, IMove
{

    public void Move(Vector3 dest)
    {
        //transform.Translate(dest);
        transform.position = dest;
    }
}
