using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveIntroContainer : MonoBehaviour
{
    [SerializeField] private GameObject waveIntro;
    [SerializeField] private GameObject bossIntro;
    [SerializeField] private GameObject bossHealthBar;
    [Header("Event Fields")]
    [SerializeField] private VoidEventSO introStart;
    [SerializeField] private VoidEventSO introBossStart;
    [SerializeField] private VoidEventSO introHalf;
    [SerializeField] private VoidEventSO introComplete;
    [SerializeField] private VoidEventSO bossDefeated;

    private bool isBossFight;

    private void Awake()
    {
        isBossFight = false;
        HideHealthBar();
        HideIntro();
    }

    private void OnEnable()
    {
        introStart.OnEventRaised += ShowIntro;
        introBossStart.OnEventRaised += ShowBossIntro;
        introComplete.OnEventRaised += HideIntro;
        introHalf.OnEventRaised += ShowHealthBar;
        bossDefeated.OnEventRaised += HideHealthBar;
    }

    private void OnDisable()
    {
        introStart.OnEventRaised -= ShowIntro;
        introBossStart.OnEventRaised -= ShowBossIntro;
        introComplete.OnEventRaised -= HideIntro;
        introHalf.OnEventRaised -= ShowHealthBar;
        bossDefeated.OnEventRaised -= HideHealthBar;
    }

    private void ShowHealthBar()
    {
        if (isBossFight)
        {
            bossHealthBar.SetActive(true);
        }
    }

    private void HideHealthBar()
    {
        bossHealthBar.SetActive(false);
    }

    private void ShowBossIntro()
    {
        bossIntro.SetActive(true);
        isBossFight = true;
    }

    private void ShowIntro()
    {
        waveIntro.SetActive(true);
    }

    private void HideIntro()
    {
        waveIntro.SetActive(false);
        bossIntro.SetActive(false);
    }
}
