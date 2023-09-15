using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDivingEnemy : EnemyBase
{
    [Header("Diving Fields")]
    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float xRange = 5.5f;
    [Header("Shooting Fields")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float shootingCooldownTime;

    private const string PLAYER_TAG = "Player";
    private enum DivingState { None, Intro, Descending, Returning, Finished }
    private DivingState state;
    private bool canAttack;

    private Transform enemyManagerTransform;
    private Vector3 positionOffset;
    private Quaternion rotationIdentity;
    private bool canShoot = true;


    private void Start()
    {
        enemyManagerTransform = transform.parent;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        canAttack = true;
        state = DivingState.None;
        positionOffset = transform.localPosition;
        rotationIdentity = transform.rotation;
    }
    protected override void Update()
    {
        base.Update();
        if (isWaveActive.value == 0f)
        {
            return;
        }
        if (canAttack && Random.Range(0, 50 + 16 * EnemyCount) == 0 && DivingEnemy.currentlyDivingEnemyCount < 5)
        {
            Attack();
        }
        if (canShoot && Random.Range(0, 100 + 30 * EnemyCount) == 0)
        {
            canShoot = false;
            Shoot();
            StartCoroutine(ResetCooldownRoutine());
        }
    }
    protected override void Attack()
    {
        canAttack = false;
        DivingEnemy.currentlyDivingEnemyCount++;
        StartCoroutine(nameof(AttackRoutine));
    }

    private void Shoot()
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

    private IEnumerator AttackRoutine()
    {
        Vector3 startingPos = transform.position;
        float yBackUpDistance = 0.2f;
        float currentSpeed = 0;
        Vector3 targetPosition = new Vector3(Random.Range(-xRange, xRange), -7f, 0f);
        while (state != DivingState.Finished)
        {
            switch (state)
            {
                case DivingState.None:
                    transform.SetParent(null);
                    state = DivingState.Intro;
                    break;
                case DivingState.Intro:
                    if (transform.position.y - startingPos.y < yBackUpDistance * 0.95f)
                    {
                        transform.position = Vector3.Lerp(transform.position, startingPos + yBackUpDistance * Vector3.up, movementSpeed * Time.deltaTime);
                    }
                    else
                    {
                        state = DivingState.Descending;
                    }
                    break;
                case DivingState.Descending:
                    if (transform.position.y > -6.5f)
                    {
                        currentSpeed = Mathf.Lerp(currentSpeed, movementSpeed, movementSpeed * Time.deltaTime);
                        transform.Translate(currentSpeed * Time.deltaTime * Vector3.down);
                        transform.Rotate(Vector3.forward, Mathf.Lerp(0, Vector3.SignedAngle(-transform.up, targetPosition - transform.position, Vector3.forward), rotateSpeed * Time.deltaTime));
                    }
                    else
                    {
                        Vector3 newPosition = transform.position;
                        newPosition.y *= -1;
                        transform.position = newPosition;
                        transform.rotation = rotationIdentity;
                        state = DivingState.Returning;
                    }
                    break;
                case DivingState.Returning:
                    if (Vector3.Distance(transform.position, enemyManagerTransform.position + positionOffset) >= 1f)
                    {
                        transform.Translate(movementSpeed * Time.deltaTime * (enemyManagerTransform.position + positionOffset - transform.position));
                    }
                    else if ((Vector3.Distance(transform.position, enemyManagerTransform.position + positionOffset) >= movementSpeed * Time.deltaTime))
                    {
                        if (transform.parent != enemyManagerTransform)
                        {
                            transform.SetParent(enemyManagerTransform);
                        }
                        transform.Translate(movementSpeed * Time.deltaTime * (enemyManagerTransform.position + positionOffset - transform.position));
                    }
                    else
                    {
                        transform.SetParent(enemyManagerTransform);
                        transform.localPosition = positionOffset;
                        state = DivingState.Finished;
                    }
                    break;
            }
            yield return null;
        }
        canAttack = true;
        DivingEnemy.currentlyDivingEnemyCount--;
    }

    protected override void DestroyEnemy()
    {
        if (canAttack)
        {
            StopCoroutine(nameof(AttackRoutine));
            DivingEnemy.currentlyDivingEnemyCount--;
        }
        if (transform.parent != enemyManagerTransform)
        {
            transform.SetParent(enemyManagerTransform);
        }
        base.DestroyEnemy();
    }
}
