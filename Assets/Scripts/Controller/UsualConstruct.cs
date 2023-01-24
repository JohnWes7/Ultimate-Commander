using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualConstruct : MonoBehaviour, IConstruct, IStop
{
    [SerializeField] private List<string> ConstructNameList;
    [SerializeField] private OptionalValue<IBeConstruct> beConstruct;
    [SerializeField] private float range
    {
        get
        {
            ISetTarget setTarget = GetComponent<ISetTarget>();
            if (setTarget != null)
            {
                return setTarget.GetRange();    
            }

            return -1f;
        }
    }

    private void Update()
    {
        TryConstructTarget();
    }

    private void TryConstructTarget()
    {
        // ������´�ָ���Ŀ�� ����Ƿ��ڹ�����������
        if (beConstruct.enabled)
        {
            // ������Χ�ƶ�
            if ((beConstruct.value.GetGameObject().transform.position - transform.position).magnitude > range)
            {
                IMove move;
                if (TryGetComponent<IMove>(out move))
                {
                    move.SetMoveDest(beConstruct.value.GetGameObject().transform.position + ((transform.position - beConstruct.value.GetGameObject().transform.position).normalized * range * 0.95f));
                }
            }
            else
            {
                beConstruct.value.BeConstruct();
                beConstruct.enabled = false;
            }
        }
    }

    public void SetConstructTarget(IBeConstruct beConstruct)
    {
        Debug.Log("UsualConstruct : SetConstructTarget ���ܽ���ָ�� " + beConstruct.GetUnitInfo().Name);
        this.beConstruct.value = beConstruct;
        this.beConstruct.enabled = true;
        beConstruct.AddConstructer(this);
    }

    public List<string> GetIConstructList()
    {
        return ConstructNameList;
    }

    private void CancelConstruct()
    {
        // ����ȡ������
        if (beConstruct.enabled)
        {
            beConstruct.value.RemoveConstructer(this);
        }
    }

    public void Stop()
    {
        // ����ȡ���ص�
        CancelConstruct();
        this.beConstruct.enabled = false;
    }

    private void OnDestroy()
    {
        CancelConstruct();   
    }

    private void OnDisable()
    {
        CancelConstruct();
    }

    /// <summary>
    /// ��ַ���ɻص�
    /// </summary>
    /// <param name="unit"></param>
    public void BeConstructToFoundation(UnitController unit)
    {
        beConstruct.enabled = false;
        ISetTarget setTarget;
        if (unit.TryGetComponent<ISetTarget>(out setTarget))
        {
            Debug.Log("UsualConstruct : BeConstructToFoundation �����Ӱģ�� ����ģ��ָ��: " + unit.name + " λ��: " + unit.transform.position);
            setTarget.SetTarget<UnitController>(unit);
        }
    }
}
