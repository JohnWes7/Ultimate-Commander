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
    [SerializeField] private HPBarController hpBarController;
    [SerializeReference] private List<object> testtemplist;

    protected virtual void Awake()
    {
        SetColor(teamColor, "colour");
    }

    private void Start()
    {
        //Debug.Log());
        //Instantiate<GameObject>(UIPrefabs.Instance.HPBarPrefab, transform.root.GetComponentInChildren<Canvas>().transform);
        hpBarController = Instantiate<GameObject>(UIPrefabs.Instance.HPBarPrefab, GameObject.Find("Canvas").transform).GetComponent<HPBarController>();
        hpBarController.Init(hp, maxHP, this);
        MonoBehaviour[] temps = GetComponents<MonoBehaviour>();
    }

    public void Init(int team, string player, string unitName, Color teamcolor, int hp, int maxHP)
    {
        this.team = team;
        this.player = player;
        this.unitName = unitName;
        this.teamColor = teamcolor;
        this.hp = hp;
        this.maxHP = maxHP;
    }

    public void SetColor(Color color, string name)
    {
        List<Transform> colours = transform.FindAllChildren(name);
        foreach (Transform item in colours)
        {
            //Debug.Log(item.name);
            MeshRenderer mr;
            if (item.TryGetComponent<MeshRenderer>(out mr))
            {
                Material temp = mr.material;
                temp.color = color;
            }
        }
        //MeshRenderer[] mrs = gameObject.GetComponentsInChildren<MeshRenderer>();
        //foreach (var mr in mrs)
        //{
        //    Material temp = mr.material;
        //    temp.color = color;
        //}
    }

    public void SetColor(Color color, Material material, string name)
    {
        List<Transform> colours = transform.FindAllChildren(name);
        material.color = color;
        foreach (Transform item in colours)
        {
            //Debug.Log(item.name);
            MeshRenderer mr;
            if (item.TryGetComponent<MeshRenderer>(out mr))
            {
                mr.material = material;
            }
        }
    }

    /// <summary>
    /// ????????????
    /// </summary>
    /// <param name="party"></param>
    public void SetTeam(int team)
    {
        this.team = team;
    }

    /// <summary>
    /// ??????????????????
    /// </summary>
    /// <param name="value"></param>
    public void SetSelectedAni(bool value)
    {
        if (value)
        {
            SetColor(Color.green, "colour");
        }
        else
        {
            SetColor(teamColor, "colour");
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
        this.hpBarController.Updatehp(hp, maxHP, this);
    }

    private void OnDestroy()
    {
        try
        {
            Destroy(hpBarController.gameObject);
        }
        catch (System.Exception)
        {

        }
        
    }
}
