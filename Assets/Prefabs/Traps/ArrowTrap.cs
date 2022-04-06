using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrowPrefab;
    public float shotInterval = 0.2f;
    private float timeSinceLastShot = 0;
    public float arrowForce = 20f;
    public int damage = 10;
    public float range = 10f;

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= shotInterval)
        {
            timeSinceLastShot = 0;
            GameObject bullet = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Projectile>().SetProjectileLifeTime(range);
            bullet.GetComponent<Projectile>().SetDamage(damage);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.up * arrowForce;
        }
        
    }
}
