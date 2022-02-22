using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    private void FixedUpdate()
    {
        // if bullet is alive for too long, destroy it
        timeAlive += Time.deltaTime;
        if (timeAlive >= timeToLive)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit!");
            other.gameObject.GetComponent<MainPlayer>().TakeDamage(damage);
        }

        Destroy(gameObject);

    }
}
