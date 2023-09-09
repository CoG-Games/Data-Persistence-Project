using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Events/Int Event")]
public class IntEventSO : ScriptableObject
{
    private List<IntEventListener> intEventListenerList;

    public void RegisterListener(IntEventListener listener)
    {
        intEventListenerList.Add(listener);
    }

    public void UnregisterListener(IntEventListener listener)
    {
        intEventListenerList.Remove(listener);
    }

    public void Invoke(int value)
    {
        foreach(IntEventListener listener in intEventListenerList)
        {
            listener.Invoke(value);
        }
    }
}
