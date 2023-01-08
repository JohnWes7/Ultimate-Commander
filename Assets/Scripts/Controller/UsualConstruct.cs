using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualConstruct : MonoBehaviour, IConstruct
{
    [SerializeField] private List<string> ConstructNameList;
    //[SerializeField] private OptionalValue<IBeConstruct> beConstruct;

    public void Construct(IBeConstruct beConstruct)
    {
        
    }

    public List<string> GetIConstructList()
    {
        return ConstructNameList;
    }
}
