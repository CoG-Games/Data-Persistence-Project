using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private FloatVariableSO healthMax;
    [SerializeField] private FloatVariableSO health;
    [SerializeField] Image healthBar;

    private void Update()
    {
        healthBar.fillAmount = Mathf.Max(health.value,0f) / healthMax.value;
    }
}
