using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class FSM<StateType>
{
    public IState curState;
    public Dictionary<StateType, IState> states;

    public FSM()
    {
        states = new Dictionary<StateType, IState>();
    }

    public void AddState(StateType stateType, IState state)
    {
        if (states.ContainsKey(stateType))
        {
            Debug.Log("FMS : AddState 已包含该key " + stateType);
            return;
        }

        states.Add(stateType, state);
    }

    public void SwitchState(StateType stateType)
    {
        if (!states.ContainsKey(stateType))
        {
            Debug.Log("FMS : SwicthState 切换失败不存在该key" + stateType);
            return;
        }
        // 调用退出回调
        if (curState != null)
        {
            curState.OnExit();
        }
        // 调用进入回调
        curState = states[stateType];
        curState.OnEnter();
    }

    public void OnUpdate()
    {
        if (curState != null)
        {
            curState.OnUpdate();
        }
    }

    public void OnFixUpdate()
    {
        if (curState != null)
        {
            curState.OnFixUpdate();
        }
    }

    public void OnCheck()
    {
        if (curState != null)
        {
            curState.OnCheck();
        }
    }
}

public interface IState
{
    public void OnEnter();
    public void OnUpdate();
    public void OnExit();
    public void OnCheck();
    public void OnFixUpdate();
}

[Serializable]
public class EventState : IState
{
    public UnityEvent onEnter;
    public UnityEvent onExit;
    public UnityEvent onUpdate;
    public UnityEvent onCheck;
    public UnityEvent onFixUpdate;

    public void OnCheck()
    {
        onCheck.Invoke();
    }

    public void OnEnter()
    {
        onEnter.Invoke();
    }

    public void OnExit()
    {
        onExit.Invoke();
    }

    public void OnFixUpdate()
    {
        onFixUpdate.Invoke();
    }

    public void OnUpdate()
    {
        onUpdate.Invoke();
    }
}