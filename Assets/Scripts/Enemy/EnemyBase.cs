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
    [SerializeField] protected IntEventSO scoreChange;

    protected int enemyHealth;

    protected void Start()
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
        scoreChange.RaiseEvent(scoreValue);
        gameObject.SetActive(false);
    }
}
