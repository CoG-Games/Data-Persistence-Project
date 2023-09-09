using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private IntEventSO OnScoreChange;

    private int score;

    private void Awake()
    {
        score = 0;
    }

    private void OnEnable()
    {
        OnScoreChange.OnEventRaised += ChangeScore;
    }
    private void OnDisable()
    {
        OnScoreChange.OnEventRaised -= ChangeScore;
    }

    private void ChangeScore(int value)
    {
        score += value;
        Debug.Log(score);
    }
}
