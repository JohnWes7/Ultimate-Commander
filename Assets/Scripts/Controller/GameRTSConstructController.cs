using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameRTSConstructController : MonoBehaviour
{
    [SerializeField] private static GameRTSConstructController instance;
    [SerializeField] public static GameRTSConstructController Instance { get => instance; }
    
    [SerializeField] public bool isConstruct { get; private set; }
    [SerializeField] public UnitInfo nowConstructUnitInfo { get; private set; }
    [SerializeField] private GameObject constructModelTemp;
    [SerializeField] private bool canConstruct;

    private void Awake()
    {
        instance = this;
        Init();
    }

    private void Init()
    {
        canConstruct = true;
        isConstruct = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isConstruct && canConstruct)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 1000f, 1 << LayerMask.NameToLayer("Ground")))
            {
                constructModelTemp.transform.position = raycastHit.point;
            }
        }

    }

    /// <summary>
    /// 设置现在建造的单位 也就是拖动一个虚影出来开始建造
    /// </summary>
    /// <param name="unitInfo"></param>
    public void SetConstructUnit(UnitInfo unitInfo)
    {
        Debug.Log("选定建造 " + unitInfo.Name);
        isConstruct = true;
        nowConstructUnitInfo = unitInfo;

        //如果连点得把上一个清除
        if (constructModelTemp)
        {
            Destroy(constructModelTemp);
        }

        // 生成新的
        constructModelTemp = Instantiate<GameObject>(unitInfo.ModelPrefab);
        // 如果是在ui框里面就先不显示
        if (!canConstruct)
        {
            constructModelTemp.SetActive(false);
        }
        
    }

    /// <summary>
    /// 关闭开启建造模式 ui遮挡的时候就不能继续建造
    /// </summary>
    /// <param name="value"></param>
    public void SetCanConstruct(bool value)
    {
        canConstruct = value;
        if (canConstruct)
        {
            if (constructModelTemp)
            {
                constructModelTemp.SetActive(true);
            }
        }
        else
        {
            if (constructModelTemp)
            {
                constructModelTemp.SetActive(false);
            }
        }
    }
}
