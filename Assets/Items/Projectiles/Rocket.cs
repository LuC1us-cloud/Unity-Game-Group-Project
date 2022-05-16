using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    public float explosionRadius;
    public LayerMask layers;

    private void FixedUpdate()
    { 
        // if bullet is alive for too long, destroy it
        timeAlive += Time.deltaTime;
        if (timeAlive >= timeToLive)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layers);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.tag == "Enemy")
                {
                    enemy.gameObject.GetComponent<Entity>().TakeDamage(damage);
                }
            }
            
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                enemy.gameObject.GetComponent<Entity>().TakeDamage(damage);
            }
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    
    }
}
