using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRTSConstructController : MonoBehaviour
{
    [SerializeField] private static GameRTSConstructController instance;
    public static GameRTSConstructController Instance { get => instance; }

    [SerializeField] private bool isConstruct;
    

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
            isConstruct = true;

        }
    }
}
