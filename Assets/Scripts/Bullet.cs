using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string targetTag;
    private float bulletSpeed;

    public void Init(string _targetTag, float _bulletSpeed)
    {
        targetTag = _targetTag;
        bulletSpeed = _bulletSpeed;
    }
    private void Update()
    {
        transform.Translate(bulletSpeed * Vector3.up * Time.deltaTime);
        if(Mathf.Abs(transform.position.y) > 7f)
        {
            Destroy(gameObject);
        }
    }
}
