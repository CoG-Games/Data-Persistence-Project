using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class EnemyBase : MonoBehaviour, IHittable
{
    public static int EnemyCount { get; protected set; }
    [Header("Enemy Stats Fields")]
    [SerializeField] protected int enemyMaxHealth;
    [SerializeField] protected int scoreValue;

    [Header("Game Fields")]
    [SerializeField] protected FloatVariableSO score;
    [SerializeField] protected FloatVariableSO scoreMultiplier;
    [SerializeField] protected FloatVariableSO isWaveActive;

    [Header("Event Fields")]
    [SerializeField] protected VoidEventSO waveCompleted;
    [SerializeField] protected VoidEventSO waveStart;
    [SerializeField] protected Vector3EventSO onDefeatedPosition;

    protected int enemyHealth;

    protected virtual void Start()
    {
        enemyHealth = enemyMaxHealth;
    }

    protected virtual void OnEnable()
    {
        EnemyCount++;
    }

    protected abstract void Attack();

    public virtual void Hit(int damageAmount)
    {
        enemyHealth -= damageAmount;
        if(enemyHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    protected virtual void DestroyEnemy()
    {
        EnemyCount--;
        score.value += scoreValue * scoreMultiplier.value;
        onDefeatedPosition.RaiseEvent(transform.position);
        gameObject.SetActive(false);
    }
}
