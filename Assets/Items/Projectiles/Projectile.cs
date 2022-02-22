using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private List<GameObject> colliders = new List<GameObject>();
    public int numberOfHits = 1;
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
        switch (other.gameObject.tag)
        {
            case "Enemy":
                other.gameObject.GetComponent<Entity>().TakeDamage(damage);
                Physics2D.IgnoreCollision(other.collider, GetComponent<CircleCollider2D>());
                break;
            case "MageEnemy":
                other.gameObject.GetComponent<mageEnemy>().TakeDamage(damage);
                break;
            default:
                break;
        }
        numberOfHits--;
        if (numberOfHits <= 0)
        {
            Destroy(gameObject);
        }
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