using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage = 10;
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
            other.gameObject.GetComponent<Entity>().TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
