using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour, IHittable
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float firingRate = 20f;
    [SerializeField] private float angleBetweenBullets = 3f;
    [SerializeField] private FloatVariableSO playerLevel;
    [SerializeField] private FloatVariableSO scoreMultiplier;

    [Header("Event Fields")]
    [SerializeField] private VoidEventSO waveCompleted;
    [SerializeField] private VoidEventSO waveStart;

    private const string ENEMY_TAG = "Enemy";
    private int bulletDamage = 1;
    private float timeToNextShot;
    private bool isWaveActive;
    void Start()
    {
        isWaveActive = false;
        timeToNextShot = 1 / firingRate;
    }

    private void OnEnable()
    {
        waveCompleted.OnEventRaised += DeactivateWave;
        waveStart.OnEventRaised += ActivateWave;
    }

    private void DeactivateWave()
    {
        isWaveActive = false;
    }

    private void ActivateWave()
    {
        isWaveActive = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!isWaveActive)
        {
            return;
        }
        if(timeToNextShot <= 0)
        {
            timeToNextShot = 1 / firingRate;
            Shoot();
        }
        timeToNextShot -= Time.deltaTime;
    }

    private void Shoot()
    {
        float yOffset = 0.2f;
        for(int i = 0; i < playerLevel.value; i++)
        {
            GameObject bulletGO = Instantiate(bulletPrefab, transform.position + yOffset * Vector3.up, bulletPrefab.transform.rotation);
            float currentRotation = angleBetweenBullets * i - (playerLevel.value-1) * angleBetweenBullets/2;
            bulletGO.transform.Rotate(Vector3.forward, currentRotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.Init(ENEMY_TAG, bulletSpeed, bulletDamage);
        }
    }

    public void Powerup()
    {
        playerLevel.value++;
    }

    public void Hit(int damageValue)
    {
        scoreMultiplier.value = 1;
        playerLevel.value = 1;
    }
}
