using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : EnemyBase
{
    [Header("Shooting Fields")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private const string PLAYER_TAG = "Player";

    private void Update()
    {
        if(Random.Range(0,500) == 0)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        float yBulletOffset = 0.1f;
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position + yBulletOffset * Vector3.down, bulletPrefab.transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.Init(PLAYER_TAG, bulletSpeed, bulletDamage);
    }
}
