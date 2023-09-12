using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 2f;

    private const string PLAYER_TAG = "Player";

    private void Update()
    {
        transform.Translate(fallSpeed * Time.deltaTime * Vector3.down);
        if (Mathf.Abs(transform.position.y) > 7f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            if(collision.gameObject.TryGetComponent<PlayerShootingManager>(out PlayerShootingManager player))
            {
                player.Powerup();
            }
            Destroy(gameObject);
        }
    }
}
