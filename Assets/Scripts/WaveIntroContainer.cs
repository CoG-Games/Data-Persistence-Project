using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveIntroContainer : MonoBehaviour
{
    [SerializeField] private GameObject waveIntro;
    [SerializeField] private GameObject bossIntro;
    [Header("Event Fields")]
    [SerializeField] private VoidEventSO introStart;
    [SerializeField] private VoidEventSO introBossStart;
    [SerializeField] private VoidEventSO introComplete;

    private void Awake()
    {
        HideIntro();
        HideBossIntro();
    }

    private void OnEnable()
    {
        introStart.OnEventRaised += ShowIntro;
        introBossStart.OnEventRaised += ShowBossIntro;
        introComplete.OnEventRaised += HideIntro;
    }
    
    private void OnDisable()
    {
        introStart.OnEventRaised -= ShowIntro;
        introBossStart.OnEventRaised -= ShowBossIntro;
        introComplete.OnEventRaised -= HideIntro;
    }

    private void ShowBossIntro()
    {
        bossIntro.SetActive(true);
    }

    private void HideBossIntro()
    {
        bossIntro.SetActive(false);
    }

    private void ShowIntro()
    {
        waveIntro.SetActive(true);
    }

    private void HideIntro()
    {
        waveIntro.SetActive(false);
    }
}
