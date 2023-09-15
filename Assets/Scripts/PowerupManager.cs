using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] private float minTimeBetweenPowerups = 1.5f;
    [SerializeField] private float playerMaxLevel = 5;
    [SerializeField] private GameObject powerupPrefab;
    [Header("Sciptable Object Fields")]
    [SerializeField] private Vector3EventSO OnEnemyDefeated;
    [SerializeField] private FloatVariableSO playerPowerupLevel;

    private bool canSpawnPowerup;

    private void Start()
    {
        canSpawnPowerup = true;   
    }

    private void OnEnable()
    {
        OnEnemyDefeated.OnEventRaised += TrySpawnPowerup;
    }

    private void OnDisable()
    {
        OnEnemyDefeated.OnEventRaised -= TrySpawnPowerup;
    }

    private void TrySpawnPowerup(Vector3 position)
    {
        if (!canSpawnPowerup || playerPowerupLevel.value == playerMaxLevel)
            return;
        if (Random.Range(0,10 + 10*(int)playerPowerupLevel.value) == 0)
        {
            Instantiate(powerupPrefab, position, powerupPrefab.transform.rotation, transform);
            canSpawnPowerup = false;
            StartCoroutine(ResetSpawningRoutine());
        }
    }

    private IEnumerator ResetSpawningRoutine()
    {
        yield return new WaitForSeconds(minTimeBetweenPowerups);
        canSpawnPowerup = true;
    }
}
