using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelController : MonoBehaviour
{
    public static GamePanelController Instance { get; private set; }

    [SerializeField] private GameObject IconPrefabs;
    [SerializeField] private Text testtext;
    [SerializeField] private List<ConstructIconController> constructIconControllersList = new List<ConstructIconController>();
    [SerializeField] private GameObject constructIconContent;
   

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateConstructIcon(List<UnitController> seletUnits)
    {
        // 所有的名字id 用于数据类去提取数据
        List<string> idlist = new List<string>();

        foreach (UnitController item in seletUnits)
        {
            // 判断每个物体是否能建造
            IConstruct construct;
            if (item.TryGetComponent<IConstruct>(out construct))
            {
                //如果能建造就获取建造的id
                List<string> templist = construct.GetIConstructList();
                foreach (string id in templist)
                {
                    if (!idlist.Contains(id))
                    {
                        idlist.Add(id);
                    }
                }
            }
        }

        // debug
        testtext.gameObject.SetActive(true);
        string test = string.Join("\n", idlist);
        if (testtext)
        {
            testtext.text = test;
        }

        // 清除之前的
        foreach (ConstructIconController item in constructIconControllersList)
        {
            //Debug.Log(item.gameObject);
            UIPool.Instance.IntoPool(item.gameObject);
        }
        constructIconControllersList.Clear();

        // 生成icon图标用来建造
        foreach (string id in idlist)
        {
            // 去数据类获取数据
            UnitInfo unitInfo = UnitPrefabs.Instance.GetUnitInfo(id);
            if (unitInfo != null)
            {
                // 如果找得到数据就开始实例化
                GameObject icon;
                // 获取对象池中的对象
                if (UIPool.Instance.TryGetGameObject(IconPrefabs.name, out icon))
                {
                    icon.transform.SetParent(constructIconContent.transform);
                }
                else
                {
                    icon = Instantiate<GameObject>(IconPrefabs, constructIconContent.transform);
                }
                ConstructIconController constructIconController = icon.GetComponent<ConstructIconController>();
                constructIconController.Init(unitInfo);
                constructIconControllersList.Add(constructIconController);
                //Debug.Log(icon.name+" "+IconPrefabs.name);
            }
        }
    }
}
