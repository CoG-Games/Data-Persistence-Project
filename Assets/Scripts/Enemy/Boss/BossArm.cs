using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArm : EnemyBase, IBossable
{
    [Header("Shooting Fields")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 4f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private int bulletCount = 30;
    [SerializeField] private float bulletSpread = 1f;
    [SerializeField] private float bulletDelayTime = 0.1f;
    [SerializeField] private float minAngle = -30f;
    [SerializeField] private float maxAngle = 75f;

    [Header("Boss Fields")]
    [SerializeField] private FloatVariableSO bossHealth;
    [SerializeField] private Color defeatedColor;
    [SerializeField] private VoidEventSO bossDefeated;

    private const string PLAYER_TAG = "Player";
    private SpriteRenderer sprite;
    private Color mainColor;
    private int hitCount;
    //private bool isDefeated;

    private void Start()
    {
        //isDefeated = false;
        hitCount = 0;
        sprite = GetComponent<SpriteRenderer>();
        mainColor = sprite.color;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        bossDefeated.OnEventRaised += DestroyEnemy;
    }

    private void OnDisable()
    {
        bossDefeated.OnEventRaised -= DestroyEnemy;
    }

    public void BossAttack()
    {
        Attack();
    }

    public int GetHealth()
    {
        return enemyHealth;
    }

    public int GetHitCount()
    {
        return hitCount;
    }

    public float GetAttackDuration()
    {
        return bulletCount * bulletDelayTime;
    }

    public bool isActive()
    {
        return gameObject.activeInHierarchy;
    }

    protected override void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    public override void Hit(int damageAmount)
    {
        if (enemyHealth > 0)
        {
            bossHealth.value -= damageAmount;
            hitCount++;
            StartCoroutine(HitRoutine());
        }
        base.Hit(damageAmount);
    }

    private IEnumerator HitRoutine()
    {
        sprite.color = Color.white;
        score.value += scoreValue * scoreMultiplier.value;
        yield return null;
        sprite.color = mainColor;
    }

    private IEnumerator AttackRoutine()
    {
        float centerAngle = (maxAngle + minAngle) / 2;
        float amplitude = (maxAngle - minAngle) / 2;
        for(int i = 0; i < bulletCount; i++)
        {
            float yBulletOffset = 0.1f;
            GameObject bulletGO = Instantiate(bulletPrefab, transform.position + yBulletOffset * Vector3.down, bulletPrefab.transform.rotation);
            float angleToRotate = -amplitude * Mathf.Cos(bulletSpread * i) + centerAngle;
            bulletGO.transform.Rotate(Vector3.forward, angleToRotate);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.Init(PLAYER_TAG, bulletSpeed, bulletDamage);

            yield return new WaitForSeconds(bulletDelayTime);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    protected override void DestroyEnemy()
    {
        //isDefeated = true;
        EnemyCount--;
        onDefeatedPosition.RaiseEvent(transform.position);
        sprite.color = defeatedColor;
        Destroy(this);
    }
}
