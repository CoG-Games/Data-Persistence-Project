using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Events/Vector3 Event")]
public class Vector3EventSO : ScriptableObject
{
    public delegate void Vector3Event(Vector3 vector3);
    public event Vector3Event OnEventRaised;

    public void RaiseEvent(Vector3 vector3)
    {
        OnEventRaised?.Invoke(vector3);
    }
}
