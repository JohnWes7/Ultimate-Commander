using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tool
{
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
}
