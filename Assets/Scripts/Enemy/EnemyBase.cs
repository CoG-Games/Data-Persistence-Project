using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Shooting Fields")]
    [SerializeField] protected GameObject bulletPrefab;

    protected Vector2 positionOffset;
    protected int enemyHealth;



    public void SetOffset()
    {
        positionOffset = transform.localPosition;
    }

    protected virtual void Shoot()
    {
        float yBulletOffset = 0.1f;
        Instantiate(bulletPrefab, transform.position + yBulletOffset * Vector3.down, bulletPrefab.transform.rotation);
    }

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
        Destroy(gameObject);
    }
}
