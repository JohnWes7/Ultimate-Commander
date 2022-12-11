using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float speed = 3f;
    [SerializeField] private bool ismoving;

    /// <summary>
    /// 设置移动方向
    /// </summary>
    /// <param name="velocity"></param>
    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
        ismoving = true;
    }

    public void Stop()
    {
        velocity = Vector3.zero;
        ismoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 移动
        if (ismoving)
        {
            transform.position += velocity * speed * Time.deltaTime;
        }
    }
}
