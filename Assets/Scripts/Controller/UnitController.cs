using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private int team;
    [SerializeField] private string player;
    [SerializeField] private string unitName;
    [SerializeField] Color teamColor;
    [SerializeField] private int hp;
    [SerializeField] private int maxHP;

    protected virtual void Awake()
    {
        SetColor(teamColor);
    }

    public void Init(int team, string player, string unitName, Color teamcolor, int hp)
    {
        this.team = team;
        this.player = player;
        this.unitName = unitName;
        this.teamColor = teamcolor;
        this.hp = hp;
    }

    public void SetColor(Color color)
    {
        MeshRenderer[] mrs = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var mr in mrs)
        {
            Material temp = mr.material;
            temp.color = color;
        }
    }

    /// <summary>
    /// 设置阵营
    /// </summary>
    /// <param name="party"></param>
    public void SetTeam(int team)
    {
        this.team = team;
    }

    /// <summary>
    /// 选中动画效果
    /// </summary>
    /// <param name="value"></param>
    public void SetSelectedAni(bool value)
    {
        if (value)
        {
            SetColor(Color.green);
        }
        else
        {
            SetColor(teamColor);
        }
    }

    public int GetTeam()
    {
        return team;
    }

    public string GetPlayer()
    {
        return player;
    }

    public int GetHP()
    {
        return hp;
    }

    public void SetHP(int hp)
    {
        this.hp = Mathf.Clamp(hp, 0, maxHP);
    }
}
