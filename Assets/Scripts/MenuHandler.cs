using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] StringVariableSO playerInitials;
    [SerializeField] TMP_InputField inputField;

    public void StartGame()
    {
        string initials = inputField.text;
        while(initials.Length < 3)
        {
            initials += "-";
        }
        playerInitials.text = initials;
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
