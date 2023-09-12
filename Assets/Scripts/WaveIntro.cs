using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveIntro : MonoBehaviour
{
    [SerializeField] private VoidEventSO readyForPreLoad;
    [SerializeField] private VoidEventSO introComplete;

    public void RaiseReadyForPreLoad()
    {
        readyForPreLoad.RaiseEvent();
    }

    public void RaiseIntroComplete()
    {
        introComplete.RaiseEvent();
    }
}
