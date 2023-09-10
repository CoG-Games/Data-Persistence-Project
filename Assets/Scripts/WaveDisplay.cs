using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] private FloatVariableSO wave;

    private TextMeshProUGUI waveText;
    private int currentWave;
    private string[] WaveDisplayArray = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "BOSS" };

    private void Start()
    {
        waveText = GetComponent<TextMeshProUGUI>();
        currentWave = 0;
    }

    private void Update()
    {
        if(currentWave != (int) wave.value)
        {
            currentWave = (int)wave.value;
            waveText.text = WaveDisplayArray[currentWave];
        }
    }
}
