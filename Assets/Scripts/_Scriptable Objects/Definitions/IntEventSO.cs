using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Events/Int Event")]
public class IntEventSO : ScriptableObject
{
    public delegate void IntEvent(int value);
    public event IntEvent OnEventRaised;

    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}
