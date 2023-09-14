using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] private FloatVariableSO wave;
    [SerializeField] private VoidEventSO introBossStart;

    private TextMeshProUGUI waveText;
    private int currentWave;
    private bool isBossActive;
    private string[] waveDisplayArray = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV" };

    private void Start()
    {
        currentWave = 0;
        isBossActive = false;
        waveText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(currentWave != (int)wave.value && !isBossActive)
        {
            currentWave = (int)wave.value;
            waveText.text = waveDisplayArray[currentWave];
        }
    }

    private void OnEnable()
    {
        introBossStart.OnEventRaised += UpdateDisplayBoss;
    }

    private void OnDisable()
    {
        introBossStart.OnEventRaised -= UpdateDisplayBoss;
    }

    private void UpdateDisplayBoss()
    {
        isBossActive = true;
        waveText.text = "BOSS";
    }
}
