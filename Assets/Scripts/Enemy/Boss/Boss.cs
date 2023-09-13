using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float extraTimeBetweenAttacks = 0.5f;
    [SerializeField] private Vector3EventSO onEnemyDefeated;
    [SerializeField] private FloatVariableSO bossHealth;
    [SerializeField] private VoidEventSO bossDefeated;
    [SerializeField] private VoidEventSO waveStarted;

    private float timeToNextAttack;
    private List<IBossable> bossComponentList;
    private int activeBossComponents;
    private bool isBossActive;
    private int lastHitCount;
    private void Start()
    {
        isBossActive = false;
        lastHitCount = 0;
        bossHealth.value = 0;
        activeBossComponents = 0;
        timeToNextAttack = extraTimeBetweenAttacks;
        bossComponentList = new List<IBossable>();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<IBossable>(out IBossable bossComponent))
            {
                bossComponentList.Add(bossComponent);
                activeBossComponents++;
                bossHealth.value = Mathf.Max(bossComponent.GetHealth(), bossHealth.value);
            }
        }
    }

    private void OnEnable()
    {
        onEnemyDefeated.OnEventRaised += DecreaseActiveComponents;
        waveStarted.OnEventRaised += ActivateBoss;
    }

    private void OnDisable()
    {
        onEnemyDefeated.OnEventRaised -= DecreaseActiveComponents;
        waveStarted.OnEventRaised -= ActivateBoss;
    }

    private void ActivateBoss()
    {
        isBossActive = true;
    }

    private void Update()
    {
        if(!isBossActive)
        {
            return;
        }
        if(bossHealth.value <= 0)
        {
            bossDefeated.RaiseEvent();
            isBossActive = false;
        }
        if(timeToNextAttack<=0)
        {
            IBossable attackingBossComponent = GetRandomComponentToAttack();
            attackingBossComponent.BossAttack();
            timeToNextAttack = attackingBossComponent.GetAttackDuration() + extraTimeBetweenAttacks;
        }
        if(timeToNextAttack > 0)
        {
            timeToNextAttack -= Time.deltaTime;
        }
        HandlePowerupGeneration();
    }

    private void HandlePowerupGeneration()
    {
        int hitCount = 0;
        foreach (IBossable bossComponent in bossComponentList)
        {
            if(bossComponent != null)
            {
                hitCount += bossComponent.GetHitCount();
            }
        }
        if(lastHitCount + 5 <= hitCount)
        {
            lastHitCount = hitCount;
            onEnemyDefeated.RaiseEvent(transform.position); //Listened to by Powerup Manager
        }
    }

    private void DecreaseActiveComponents(Vector3 ignore)
    {
        bossComponentList.Clear();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<IBossable>(out IBossable bossComponent))
            {
                bossComponentList.Add(bossComponent);
            }
        }
    }

    private IBossable GetRandomComponentToAttack()
    {
        int randomIndex = Random.Range(0, bossComponentList.Count);
        return bossComponentList[randomIndex];
    }
}
