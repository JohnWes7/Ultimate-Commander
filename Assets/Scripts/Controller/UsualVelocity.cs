using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class UsualVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private OptionalValue<Vector3> velocity;
    [SerializeField] private float speed;

    /// <summary>
    /// 设置移动方向
    /// </summary>
    /// <param name="velocity"></param>
    public void SetVelocity(Vector3 velocity)
    {
        this.velocity.value = velocity;
        this.velocity.enabled = true;

        transform.DOKill();
        transform.DORotateQuaternion(Quaternion.LookRotation(velocity, Vector3.up), 0.3f);
    }

    public void Stop()
    {
        velocity.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 移动
        if (velocity.enabled)
        {
            transform.position += velocity.value * speed * Time.deltaTime;
        }
    }
}
