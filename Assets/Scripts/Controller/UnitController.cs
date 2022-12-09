using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private int party;
    [SerializeField]
    Color teamColor;

    protected virtual void Awake()
    {
        SetColor(teamColor);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public void SetColor(Color color)
    {
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        Material temp = mr.material;
        temp.color = color;

    }

    /// <summary>
    /// 设置阵营
    /// </summary>
    /// <param name="party"></param>
    public void SetParty(int party)
    {
        this.party = party;
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
}
