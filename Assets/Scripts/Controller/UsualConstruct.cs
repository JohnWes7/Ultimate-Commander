using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualConstruct : MonoBehaviour, IConstruct, IStop
{
    [SerializeField] private List<string> ConstructNameList;
    [SerializeField] private OptionalValue<IBeConstruct> beConstruct;

    private void Update()
    {
           
    }

    private void TryConstructTarget()
    {

    }

    public void Construct(IBeConstruct beConstruct)
    {
        this.beConstruct.value = beConstruct;
        this.beConstruct.enabled = true;
    }

    public List<string> GetIConstructList()
    {
        return ConstructNameList;
    }

    public void Stop()
    {
        this.beConstruct.enabled = false;
    }
}
