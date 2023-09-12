using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMultiplierDisplay : MonoBehaviour
{

    [SerializeField] private FloatVariableSO scoreMultiplier;

    [Header("Animation Fields")]
    [SerializeField] private float rotationAmplitude = 1f;
    [SerializeField] private float rotationFrequency = 1f;
    [SerializeField] private float scaleAmplitude = 1f;
    [SerializeField] private float scaleFrequency = 1f;

    private TextMeshProUGUI multiplierText;
    private int lastMultiplier = 0;
    // Start is called before the first frame update
    void Start()
    {
        multiplierText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lastMultiplier != (int)scoreMultiplier.value)
        {
            lastMultiplier = (int)scoreMultiplier.value;
            multiplierText.text = "x" + lastMultiplier;
        }
        Animate();
    }

    private void Animate()
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.forward, rotationAmplitude*Mathf.Sin(rotationFrequency*Time.time));
        transform.localScale = (scaleAmplitude/100f * Mathf.Sin(scaleFrequency * Time.time) + 1) * Vector3.one;
    }
}
