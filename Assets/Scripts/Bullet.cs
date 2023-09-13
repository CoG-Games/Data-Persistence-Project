using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string targetTag;
    private float bulletSpeed;
    private int damage;

    public void Init(string _targetTag, float _bulletSpeed, int _damage)
    {
        targetTag = _targetTag;
        bulletSpeed = _bulletSpeed;
        damage = _damage;
    }
    private void Update()
    {
        transform.Translate(bulletSpeed * Vector3.up * Time.deltaTime);
        if(Mathf.Abs(transform.position.y) > 7f || Mathf.Abs(transform.position.x) > 7f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(targetTag))
        {
            if(collision.gameObject.TryGetComponent<IHittable>(out IHittable hittable))
            {
                hittable.Hit(damage);
            }
            Destroy(gameObject);
        }
    }
}
