using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Events/Void Event")]
public class VoidEventSO : ScriptableObject
{
    public delegate void VoidEvent();
    public event VoidEvent OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
