using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMain : EnemyBase, IBossable
{
    [Header("Shooting Fields")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 4f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float angleBetweenBullets = 6f;
    [SerializeField] private int majorBulletCount = 10;
    [SerializeField] private int bulletWaveCount = 3;
    [SerializeField] private float timeBetweenWaves = 0.5f;
    [SerializeField] private float yOffset = -1f;

    [Header("Boss Fields")]
    [SerializeField] private FloatVariableSO bossHealth;
    [SerializeField] private Color defeatedColor;
    [SerializeField] private VoidEventSO bossDefeated;

    private const string PLAYER_TAG = "Player";
    private SpriteRenderer sprite;
    private Color mainColor;
    private int hitCount;
    private bool isDefeated;

    private void Start()
    {
        isDefeated = false;
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
        return timeBetweenWaves * bulletWaveCount;
    }

    public bool isActive()
    {
        return gameObject.activeInHierarchy;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        if (isDefeated)
        {
            return;
        }
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
        for(int bulletWave = 0; bulletWave < bulletWaveCount; bulletWave++)
        {
            for (int i = 0; i < majorBulletCount - bulletWave%2; i++)
            {
                GameObject bulletGO = Instantiate(bulletPrefab, transform.position + yOffset * Vector3.up, bulletPrefab.transform.rotation);
                float currentRotation = angleBetweenBullets * i - (majorBulletCount - bulletWave % 2 - 1) * angleBetweenBullets / 2;
                bulletGO.transform.Rotate(Vector3.forward, currentRotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                bullet.Init(PLAYER_TAG, bulletSpeed, bulletDamage);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    protected override void DestroyEnemy()
    {
        isDefeated = true;
        EnemyCount--;
        onDefeatedPosition.RaiseEvent(transform.position);
        StopAllCoroutines();
        sprite.color = defeatedColor;
        Destroy(this);
    }
}
