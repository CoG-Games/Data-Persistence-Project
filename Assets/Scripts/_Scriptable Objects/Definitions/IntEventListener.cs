using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEventListener : MonoBehaviour
{
    [SerializeField] private IntEventSO intEvent;
    [SerializeField] private UnityEvent response;

    private void OnEnable()
    {
        intEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        intEvent.UnregisterListener(this);
    }

    public void Invoke(int value)
    {
        response?.Invoke();
    }
}
