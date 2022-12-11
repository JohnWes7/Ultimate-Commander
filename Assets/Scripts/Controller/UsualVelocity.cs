using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualVelocity : MonoBehaviour, IMoveVelocity
{
    private Vector3 velocity;
    [SerializeField] private float speed = 0.5f;

    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * speed * Time.deltaTime;
    }
}
