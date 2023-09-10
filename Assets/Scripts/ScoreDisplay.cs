using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private FloatVariableSO score;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float oldScore;

    private void Update()
    {
        if(score.value != oldScore)
        {
            oldScore = score.value;
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
}