using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tool
{
    /// <summary>
    /// 弃用的
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static List<Transform> GetAllChildren(Transform parent)
    {
        List<Transform> ans = new List<Transform>();

        if (parent.childCount == 0)
        {
            ans.Add(parent);
            return ans;
        }

        foreach (Transform item in parent)
        {
            ans.AddRange(GetAllChildren(item));
        }
        return ans;
    }

    public static List<Transform> FindAllChildren(this Transform transform, string name)
    {
        List<Transform> ans = new List<Transform>();
  
        if (transform.name == name)
        {
            ans.Add(transform);
            return ans;
        }

        foreach (Transform item in transform)
        {
            ans.AddRange(item.FindAllChildren(name));
        }
        return ans;
    }
}
