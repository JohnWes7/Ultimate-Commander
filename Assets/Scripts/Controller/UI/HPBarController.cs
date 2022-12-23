using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    [SerializeField] private Image hpbarFront;
    [SerializeField] private UnitController unitController;
    [SerializeField] private int offset = 0;

    public void Init(int hp, int maxhp, UnitController unitController, int offset = 0)
    {
        this.unitController = unitController;
        hpbarFront.fillAmount = hp / (float)maxhp;
        this.offset = offset;
    }

    public void Updatehp(int hp, int maxhp, UnitController unitController)
    {
        this.unitController = unitController;
        hpbarFront.fillAmount = hp / (float)maxhp;
    }

    void Update()
    {
        if (unitController)
        {
            transform.position = Camera.main.WorldToScreenPoint(unitController.transform.position);
        }
    }
}
