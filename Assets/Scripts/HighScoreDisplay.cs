using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private StringVariableSO playerInitials;
    [SerializeField] private FloatVariableSO playerScore;

    [Header("High Score Fields")]
    [SerializeField] private List<TextMeshProUGUI> textHighScoreInitialList;
    [SerializeField] private List<TextMeshProUGUI> textHighScoreList;

    private string[] highScoreInitialArray;
    private string[] highScoreArray;
    private bool hasNewHighScore;

    private void Awake()
    {
        hasNewHighScore = false;
        highScoreInitialArray = new string[textHighScoreInitialList.Count];
        highScoreArray = new string[textHighScoreInitialList.Count];
        LoadHighScores();
        int newHighScoreIndex = 0;
        for(int i = 0; i < textHighScoreInitialList.Count; i++)
        {
            if((int)playerScore.value >= int.Parse(highScoreArray[i]))
            {
                break;
            }
            newHighScoreIndex++;
        }
        if(newHighScoreIndex < textHighScoreInitialList.Count)
        {
            hasNewHighScore = true;
            //string tempInitials = highScoreInitialArray[newHighScoreIndex];
            //string tempScore = highScoreArray[newHighScoreIndex];
            //highScoreInitialArray[newHighScoreIndex] = playerInitials.text;
            //highScoreArray[newHighScoreIndex] = GetScoreText((int)playerScore.value);
            for(int j = textHighScoreInitialList.Count-1; j>newHighScoreIndex ; j--)
            {
                highScoreInitialArray[j] = highScoreInitialArray[j-1];
                highScoreArray[j] = highScoreArray[j-1];
            }
            highScoreInitialArray[newHighScoreIndex] = playerInitials.text;
            highScoreArray[newHighScoreIndex] = GetScoreText((int)playerScore.value);
        }
        for (int i = 0; i < textHighScoreInitialList.Count; i++)
        {
            textHighScoreInitialList[i].text = highScoreInitialArray[i];
            textHighScoreList[i].text = highScoreArray[i];
        }
        if(hasNewHighScore)
        {
            SaveHighScores();
        }
    }

    private string GetScoreText(int score)
    {
        string oldText = score.ToString();
        string newText = "";
        while(oldText.Length + newText.Length < 7)
        {
            newText += "0";
        }
        newText += oldText;
        return newText;
    }

    [System.Serializable]
    class SaveData
    {
        public string[] initials;
        public string[] scores;
    }

    public void SaveHighScores()
    {
        SaveData data = new SaveData();
        data.initials = new string[textHighScoreInitialList.Count];
        data.scores = new string[textHighScoreInitialList.Count];
        for(int i = 0; i < textHighScoreInitialList.Count; i++)
        {
            data.initials[i] = highScoreInitialArray[i];
            data.scores[i] = highScoreArray[i];
        }
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            for (int i = 0; i < highScoreArray.Length; i++)
            {
                highScoreInitialArray[i] = data.initials[i];
                highScoreArray[i] = data.scores[i];
            }
        }
        else
        {
            for (int i = 0; i < highScoreArray.Length; i++)
            {
                highScoreInitialArray[i] = "COG";
                highScoreArray[0] = "0000000";
            }
            highScoreArray[0] = "5555555";
            highScoreArray[1] = "3141593";
            highScoreArray[2] = "0987654";
        }
    }
}
