using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitialInputHandler : MonoBehaviour
{
    private TMP_InputField intialInputField;

    private void Awake()
    {
        intialInputField = GetComponent<TMP_InputField>();
    }

    public void CaptilizeLetters()
    {
        intialInputField.text = intialInputField.text.ToUpper();
    }
}
