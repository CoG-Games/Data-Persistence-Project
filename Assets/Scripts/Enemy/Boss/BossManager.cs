using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;

    [Header("Event Fields")]
    [SerializeField] private VoidEventSO introBossStart;
    [SerializeField] private VoidEventSO introHalf;

    private bool isBossWave;

    private void Awake()
    {
        isBossWave = false;
    }

    private void OnEnable()
    {
        introBossStart.OnEventRaised += SetBossWave;
        introHalf.OnEventRaised += SpawnBoss;
    }

    private void OnDisable()
    {
        introBossStart.OnEventRaised -= SetBossWave;
        introHalf.OnEventRaised -= SpawnBoss;
    }

    private void SetBossWave()
    {
        isBossWave = true;
    }

    private void SpawnBoss()
    {
        if(isBossWave)
        {
            Instantiate(bossPrefab, bossPrefab.transform.position, bossPrefab.transform.rotation, transform);
        }
    }
}
