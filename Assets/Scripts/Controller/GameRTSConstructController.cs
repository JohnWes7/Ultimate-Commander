using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameRTSConstructController : MonoBehaviour
{
    [SerializeField] private static GameRTSConstructController instance;
    [SerializeField] public static GameRTSConstructController Instance { get => instance; }
    
    [SerializeField] public bool isConstruct { get; private set; }
    [SerializeField] private GameObject ModelTemp;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 暂时用q来表示进入建造模式
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isConstruct = !isConstruct;
        }

        if (isConstruct)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 1000f, 1 << LayerMask.NameToLayer("Ground")))
            {
                
            }
        }

    }
}
