using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameRTSConstructController : MonoBehaviour
{
    [SerializeField] private static GameRTSConstructController instance;
    [SerializeField] public static GameRTSConstructController Instance { get => instance; }
    [SerializeField] private bool isConstruct;
    [SerializeField] public bool IsConstruct { get => isConstruct; private set => isConstruct = value; }
    [SerializeField] public UnitInfo curUnitInfo { get; private set; }
    [SerializeField] private GameObject constructModelTemp;
    [SerializeField] private GameObject baseConstructObject;
    [SerializeField] private Material preSetMat;
    [SerializeField, ColorUsage(true, true)] private Color preSetMatColor;
    [SerializeField, ColorUsage(true, true)] private Color preSetMatNagetiveColor; 
    [SerializeField] private bool canConstruct;

    private void Awake()
    {
        instance = this;
        Init();
    }

    private void Init()
    {
        canConstruct = true;
        IsConstruct = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 建造
        if (IsConstruct && canConstruct)
        {
            // 虚影跟随鼠标移动
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 1000f, 1 << LayerMask.NameToLayer("Ground")))
            {
                if (constructModelTemp)
                {
                    constructModelTemp.transform.position = raycastHit.point;
                }

                // 左键开始建造 TODO : 建造碰撞检测
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    GameObject obj = Instantiate(baseConstructObject, raycastHit.point, Quaternion.identity);
                    GameObject model = Instantiate<GameObject>(curUnitInfo.ModelPrefab, obj.transform);
                    // 初始化建造基址
                    obj.GetComponent<IBeConstruct>().Init(curUnitInfo, GameRTSController.Instance.GetTeam(), GameRTSController.Instance.GetPlayer());
                    // 更改物体的材质和颜色变为虚影
                    SetMatForAllChildren(model, preSetMat, preSetMatColor);

                    // 下达建造指令
                    GameRTSController.Instance.OrderConstruct(obj.GetComponent<IBeConstruct>());

                    // 退出建造
                    CancelConstruct();
                }
            }

            // 右键取消建造
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CancelConstruct();
            }
        }

    }

    private void CancelConstruct()
    {
        // 晚一帧设置为false 代表这一帧还算是在建造中
        Debug.Log("CRCC : Update 退出建造" + curUnitInfo.Name);
        StartCoroutine(WaitOneFrame2SetisConstruct());
        Destroy(constructModelTemp);
    }

    private IEnumerator WaitOneFrame2SetisConstruct()
    {
        yield return new WaitForEndOfFrame();

        IsConstruct = false;
    }

    /// <summary>
    /// 设置现在建造的单位 也就是拖动一个虚影出来开始建造 给ui进行触发
    /// </summary>
    /// <param name="unitInfo"></param>
    public void SetConstructUnit(UnitInfo unitInfo)
    {
        Debug.Log("CRCC : SetConstructUnit 选定建造 " + unitInfo.Name);
        IsConstruct = true;
        curUnitInfo = unitInfo;

        // 如果连点得把上一个清除
        if (constructModelTemp)
        {
            Destroy(constructModelTemp);
        }

        // 生成新的
        constructModelTemp = Instantiate<GameObject>(unitInfo.ModelPrefab);
        // 更改物体的材质和颜色变为虚影
        SetMatForAllChildren(constructModelTemp, preSetMat, preSetMatColor);

        // 如果是在ui框里面就先不显示
        if (!canConstruct)
        {
            constructModelTemp.SetActive(false);
        }
    }

    /// <summary>
    /// 关闭开启建造模式 **并且隐藏虚影建筑** ui遮挡的时候就不能继续建造
    /// </summary>
    /// <param name="value"></param>
    public void SetConstructModel(bool value)
    {
        canConstruct = value;

        // 如果是开启建造模式
        if (canConstruct)
        {
            if (constructModelTemp)
            {
                constructModelTemp.SetActive(true);
            }
        }

        // 如果是关闭建造模式
        else
        {
            if (constructModelTemp)
            {
                constructModelTemp.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 给物体下的所有带有渲染的地方替换材质和颜色
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="material"></param>
    /// <param name="color"></param>
    public void SetMatForAllChildren(GameObject gameObject, Material material, Color color)
    {
        // 获取所有子物体
        List<Transform> gobjList = Tool.GetAllChildren(gameObject.transform);
        Color originColor = material.color;
        material.color = color;

        // 给所有物体上色
        foreach (Transform item in gobjList)
        {
            MeshRenderer meshRenderer;
            if (item.TryGetComponent<MeshRenderer>(out meshRenderer))
            {
                meshRenderer.material = material;
            }
        }

        material.color = originColor;
    }
}
