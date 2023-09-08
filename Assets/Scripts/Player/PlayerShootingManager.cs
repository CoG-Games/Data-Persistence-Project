using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float firingRate = 20f;
    [SerializeField] private float angleBetweenBullets = 3f;
    [SerializeField] private FloatVariableSO playerLevel;

    private const string ENEMY_TAG = "Enemy";
    private float timeToNextShot;
    void Start()
    {
        timeToNextShot = 1 / firingRate;
    }

    // Update is called once per frame
    private void Update()
    {
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
            bullet.Init(ENEMY_TAG, bulletSpeed);
        }
    }
}
