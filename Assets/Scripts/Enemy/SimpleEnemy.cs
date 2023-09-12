using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : EnemyBase
{
    [Header("Shooting Fields")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float shootingCooldownTime;

    private const string PLAYER_TAG = "Player";
    private bool canShoot = true;

    private void Update()
    {
        //Debug.Log(base.isWaveActive);
        if (isWaveActive.value == 0f)
        {
            return;
        }
        if (canShoot && Random.Range(0,200+30* EnemyCount) == 0)
        {
            canShoot = false;
            Attack();
            StartCoroutine(ResetCooldownRoutine());
        }
    }

    protected override void Attack()
    {
        float yBulletOffset = 0.1f;
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position + yBulletOffset * Vector3.down, bulletPrefab.transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.Init(PLAYER_TAG, bulletSpeed, bulletDamage);
    }

    private IEnumerator ResetCooldownRoutine()
    {
        yield return new WaitForSeconds(shootingCooldownTime);
        canShoot = true;
    }
}
