using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;

public class UnitPrefabs : Single<UnitPrefabs>
{
    public GameObject FactoryPrefab { get; }
    [JsonProperty] public Dictionary<string, UnitInfo> UnitInfoDict { get; private set; }
    public Color DefaultTeamColor;

    public UnitPrefabs()
    {
        // 初始化杂项
        DefaultTeamColor = Color.blue;

        // 初始化数据类
        string json = Tool.ReadTextFromResourcesJSON(ResourcesPath.BuildingData_Path);
        UnitInfoDict = Tool.JSONString2Object<Dictionary<string, UnitInfo>>(json);

        //把导入的每一条数据初始化
        foreach (var item in UnitInfoDict)
        {
            item.Value.InitAndLoadPrefab(item.Key);
        }
        Debug.Log("UnitPrefabs : \n" + json + "\nUnitPrefabs 初始化成功");
        Debug.Log("UnitPrefabs : 一共" + UnitInfoDict.Keys.Count.ToString() + "条数据\n分别为:\n" + string.Join("\n",UnitInfoDict.Keys));
    }

    /// <summary>
    /// 获取某个单位的数据
    /// </summary>
    /// <param name="keyname"></param>
    /// <returns></returns>
    public UnitInfo GetUnitInfo(string keyname)
    {
        UnitInfo unitInfo;
        if (UnitInfoDict.TryGetValue(keyname, out unitInfo))
        {
            return unitInfo;
        }

        Debug.Log("获取 " + keyname + " 失败");
        return null;
    }

    public void TestClassToJson()
    {
        //UnitInfoDict = new Dictionary<string, UnitInfo>();
        //UnitInfo temp = new UnitInfo();
        //temp.ModelPrefabPath = ResourcesPath.Factory_PATH;
        //temp.PrefabPath = ResourcesPath.Factory_PATH;
        //UnitInfoDict.Add("factory", temp);
        ////JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        ////jsonSerializerSettings.
        //Debug.Log(JsonConvert.SerializeObject(UnitInfoDict));
        
    }
}

// unit 数据
[JsonObject(MemberSerialization.OptIn)]
[Serializable]
public class UnitInfo
{
    private static string NonePrefab = ResourcesPath.NonePrefab;
    private static string NoneIcon = ResourcesPath.NoneIconPath;

    [JsonProperty] public string model_prefab_path;
    [JsonProperty] public string prefab_path;
    [JsonProperty] public string icon_path;
    [JsonProperty] private int max_hp;
    [SerializeField] private string name;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject modelPrefab;
    [SerializeField] private Sprite sprite;

     public string Name { get => name; private set => name = value; }
     public GameObject Prefab { get => prefab; private set => prefab = value; }

     public GameObject ModelPrefab { get => modelPrefab; private set => modelPrefab = value; }

     public Sprite Sprite { get => sprite; private set => sprite = value; }

     public int MaxHp { get => max_hp; private set => max_hp = value; }

    /// <summary>
    /// 初始化数据: 赋予key名字为单位名字 以及加载路径里面的预制体和图片
    /// </summary>
    /// <param name="key"></param>
    /// <param name="fromResources"></param>
    public void InitAndLoadPrefab(string key, bool fromResources = true)
    {
        // 用key作为名字保证名字唯一
        this.Name = key;

        if (fromResources)
        {
            // 载入模型
            Prefab = Resources.Load<GameObject>(prefab_path);
            if (Prefab == null)
            {
                Prefab = Resources.Load<GameObject>(NonePrefab);
                Debug.Log("未找到模型: " + prefab_path);
                if (Prefab == null)
                {
                    Debug.LogError("Error 导入默认模型出错");
                }
            }

            ModelPrefab = Resources.Load<GameObject>(model_prefab_path);
            if (ModelPrefab == null)
            {
                ModelPrefab = Resources.Load<GameObject>(NonePrefab);
                Debug.Log("未找到模型: " + model_prefab_path);
                if (ModelPrefab == null)
                {
                    Debug.LogError("Error 导入默认模型出错");
                }
            }

            //载入图片
            Sprite = Resources.Load<Sprite>(icon_path);
            if (Sprite == null)
            {
                Sprite = Resources.Load<Sprite>(NoneIcon);
                Debug.Log("未找到Icon: " + icon_path);
                if (ModelPrefab == null)
                {
                    Debug.LogError("Error 导入默认Icon出错");
                }
            }
        }
        else
        {
            Debug.LogError("不支持从另外地方导入");
        }
    }
}
