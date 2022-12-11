using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualMove : MonoBehaviour, IMove
{
    private Vector3 dest;
    private IMoveVelocity moveVelocity;
    private bool ismoving = false;

    private void Awake()
    {
        moveVelocity = GetComponent<IMoveVelocity>();
    }

    public void SetMoveDest(Vector3 dest)
    {
        //transform.Translate(dest);
        this.dest = dest;
        ismoving = true;
    }

    private void Update()
    {
        // 监测是否到达目的地
        if (!ismoving || (dest - transform.position).magnitude < 0.01f)
        {
            if (moveVelocity != null)
            {
                moveVelocity.Stop();
            }
            ismoving = false;
            return;
        }

        // 如果没到达就向目的地移动
        if (moveVelocity != null)
        {
            Vector3 direction = (dest - transform.position).normalized;
            moveVelocity.SetVelocity(direction);
        }
    }

    public void Stop()
    {
        dest = transform.position;
        ismoving = false;
    }
}
