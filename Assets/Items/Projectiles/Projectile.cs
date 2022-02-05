using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private float timeAlive = 0;
    private float timeToLive = 10;
    private void FixedUpdate()
    {
        // if bullet is alive for too long, destroy it
        timeAlive += Time.deltaTime;
        if (timeAlive >= timeToLive)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Entity>().TakeDamage(damage);
        }
        if (other.gameObject.tag == "MageEnemy")
        {
            other.gameObject.GetComponent<mageEnemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
    public void SetProjectileLifeTime(float time)
    {
        timeToLive = time;
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
