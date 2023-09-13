using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatVariableSO score;
    [SerializeField] private FloatVariableSO wave;
    [SerializeField] private FloatVariableSO scoreMultiplier;
    [SerializeField] private FloatVariableSO waveCount;

    [Header("Event Fields")]
    [SerializeField] private VoidEventSO introStart;
    [SerializeField] private VoidEventSO introBossStart;
    [SerializeField] private VoidEventSO introHalf;
    [SerializeField] private VoidEventSO introComplete;
    [SerializeField] private VoidEventSO waveCompleted;
    [SerializeField] private VoidEventSO preWaveStart;
    [SerializeField] private VoidEventSO waveStart;
    [SerializeField] private VoidEventSO bossDefeated;

    private bool isWaveActive;

    private void Awake()
    {
        score.value = 0;
        wave.value = 0;
        scoreMultiplier.value = 1;
        isWaveActive = false;
    }

    private void OnEnable()
    {
        introHalf.OnEventRaised += StartPreWave;
        introComplete.OnEventRaised += CompleteIntro;
        waveCompleted.OnEventRaised += CompleteWave;
        bossDefeated.OnEventRaised += WinLevel;
    }

    private void WinLevel()
    {
        Debug.Log("YOU WIN!!!");
    }

    private void OnDisable()
    {
        introHalf.OnEventRaised -= StartPreWave;
        introComplete.OnEventRaised -= CompleteIntro;
    }

    private void StartPreWave()
    {
        if (wave.value <= waveCount.value +1)
        {
            preWaveStart.RaiseEvent();
        }
    }

    private void CompleteIntro()
    {
        if (wave.value <= waveCount.value +1)
        {
            waveStart.RaiseEvent();
        }
    }

    private void CompleteWave()
    {
        scoreMultiplier.value++;
        StartNext();
    }

    private void Update()
    {
        if(!isWaveActive)
        {
            isWaveActive = true;
            StartNext();
        }
    }

    private void StartNext()
    {
        if(wave.value < waveCount.value)
        {
            wave.value++;
            introStart.RaiseEvent();
        }
        else if(wave.value == waveCount.value)
        {
            wave.value++;
            introBossStart.RaiseEvent();
        }
    }
}
