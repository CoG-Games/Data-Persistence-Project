using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private FloatVariableSO score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private bool isStatic;

    private int lastScore;
    private void Awake()
    {
        lastScore = -1;
        if(isStatic)
        {
            string oldText = Mathf.Min(score.value, 9999999).ToString();
            string newText = "";
            while (oldText.Length + newText.Length < 7)
            {
                newText += "0";
            }
            newText += oldText;
            scoreText.text = newText;
        }
    }

    private void Update()
    {
        if(isStatic)
        {
            return;
        }
        if(lastScore != (int)score.value)
        {
            lastScore = (int)score.value;
            string oldText = Mathf.Min(lastScore, 9999999).ToString();
            string newText = "";
            while (oldText.Length + newText.Length < 7)
            {
                newText += "0";
            }
            newText += oldText;
            scoreText.text = newText;
        }
    }
}
