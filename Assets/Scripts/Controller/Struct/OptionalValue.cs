using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct OptionalValue<T>
{
    [SerializeField] public T value;
    [SerializeField] public bool enabled;

    public OptionalValue(T value)
    {
        this.enabled = true;
        this.value = value;
    }
}
