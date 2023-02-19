using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRepireLaz : MonoBehaviour, IFire
{
    [SerializeField] private UnitController from;
    [SerializeField] private UnitController target;
    [SerializeField] private int damage;
    [SerializeField] private bool lazing = false;
    [SerializeField] private bool beCall = false;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject FXPrefab;
    [SerializeField] private ParticleSystem FX;

    private void Init()
    {
        firePos = FireTankBullet.FindBulletFirePos(transform);
        FX = Instantiate<GameObject>(FXPrefab, firePos).GetComponent<ParticleSystem>();
        FX.transform.localPosition = Vector3.zero;
        FX.Stop();
    }

    private void Start()
    {
        Init();
    }

    // 因为是激光类会被每一帧调用
    public void Fire(UnitController unit, UnitController from, int damage, params object[] values)
    {
        this.from = from;
        this.target = unit;
        this.damage = damage;
        beCall = true;
        lazing = true;
        if (!FX.isPlaying)
        {
            FX.Play();
        }
    }

    // 每一个fixedupdate修一下0.02秒修一下
    private void FixedUpdate()
    {
        if (lazing)
        {
            RepireLaz();
        }
    }

    private void RepireLaz()
    {
        Debug.Log("FireRepireLaz : RepireLaz 正在修复");

        IBeRepaired targetBeRepaire;
        if (target.TryGetComponent<IBeRepaired>(out targetBeRepaire))
        {
            targetBeRepaire.IBeRepaired(damage);
        }
        
    }

    // 在稍后update里面判断
    private void LateUpdate()
    {
        // 如果这一帧调用了 重置flag
        if (beCall == true)
        {
            beCall = false;
        }
        // 如果这一帧没有调用说明停止
        else
        {
            lazing = false;
            FX.Stop();
        }
    }
}
