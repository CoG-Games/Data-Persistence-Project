using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatVariableSO score;
    [SerializeField] private FloatVariableSO wave;

    private void Awake()
    {
        score.value = 0;
        wave.value = 0;
    }

    
}
