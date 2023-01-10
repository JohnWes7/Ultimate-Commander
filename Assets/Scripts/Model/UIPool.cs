using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPool : MonoBehaviour
{
    public static UIPool Instance { get; private set; }

    [SerializeReference] public Dictionary<string, List<GameObject>> pool = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 进入缓冲池
    /// </summary>
    /// <param name="gameObject"></param>
    public void IntoPool(GameObject gameObject)
    {
        gameObject.transform.SetParent(transform);
        gameObject.SetActive(false);

        if (pool.ContainsKey(gameObject.name))
        {
            pool[gameObject.name].Add(gameObject);
        }
        else
        {
            pool.Add(gameObject.name, new List<GameObject>());
            pool[gameObject.name].Add(gameObject);
        }
    }

    /// <summary>
    /// 获得池子里的实例
    /// </summary>
    /// <param name="prefabNameNoClone"></param>
    /// <returns></returns>
    public bool TryGetGameObject(string prefabNameNoClone, out GameObject result)
    {
        //foreach (var item in pool)
        //{
        //    Debug.Log(item.Key + ":" + item.Value.Count);
        //}
        result = null;
        prefabNameNoClone += "(Clone)";

        if (pool.ContainsKey(prefabNameNoClone) && pool[prefabNameNoClone].Count > 0)
        {
            result = pool[prefabNameNoClone][0];
            result.SetActive(true);
            pool[prefabNameNoClone].RemoveAt(0);
            return true;
        }

        return false;
    }
}
