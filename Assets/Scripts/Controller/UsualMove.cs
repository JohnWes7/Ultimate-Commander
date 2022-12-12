using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualMove : MonoBehaviour, IMove
{
    [SerializeField] private OptionalValue<Vector3> dest;
    private IMoveVelocity moveVelocity;

    private void Awake()
    {
        moveVelocity = GetComponent<IMoveVelocity>();
        //dest = new OptionalValue<Vector3>(Vector3.zero);
        //dest.enabled = false;
    }

    public void SetMoveDest(Vector3 dest)
    {
        //transform.Translate(dest);
        this.dest.value = dest;
        this.dest.enabled = true;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 监测是否到达目的地
        if (!dest.enabled)
        {
            return;
        }

        else if ((dest.value - transform.position).magnitude < 0.01f)
        {
            if (moveVelocity != null)
            {
                moveVelocity.Stop();
            }
            Stop();
        }

        // 如果没到达就向目的地移动
        else if (moveVelocity != null)
        {
            Vector3 direction = (dest.value - transform.position).normalized;
            moveVelocity.SetVelocity(direction);
        }
    }

    public void Stop()
    {
        dest.enabled = false;
    }
}
