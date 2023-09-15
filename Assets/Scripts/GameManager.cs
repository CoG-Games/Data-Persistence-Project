using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatVariableSO score;
    [SerializeField] private FloatVariableSO wave;
    [SerializeField] private FloatVariableSO scoreMultiplier;
    [SerializeField] private FloatVariableSO waveCount;
    [SerializeField] private FloatVariableSO playerLevel;

    [Header("Event Fields")]
    [SerializeField] private VoidEventSO introStart;
    [SerializeField] private VoidEventSO introBossStart;
    [SerializeField] private VoidEventSO introHalf;
    [SerializeField] private VoidEventSO introComplete;
    [SerializeField] private VoidEventSO waveCompleted;
    [SerializeField] private VoidEventSO preWaveStart;
    [SerializeField] private VoidEventSO waveStart;
    [SerializeField] private VoidEventSO levelVictory;


    private bool isWaveActive;

    private void Awake()
    {
        playerLevel.value = 1;
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
        levelVictory.OnEventRaised += WinLevel;
    }

    private void WinLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name + 1);
    }

    private void OnDisable()
    {
        introHalf.OnEventRaised -= StartPreWave;
        introComplete.OnEventRaised -= CompleteIntro;
        waveCompleted.OnEventRaised -= CompleteWave;
        levelVictory.OnEventRaised -= WinLevel;
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
