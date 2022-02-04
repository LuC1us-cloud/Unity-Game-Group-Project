using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shotInterval = 0.2f;
    private float timeSinceLastShot = 0;

    public float bulletForce = 20f;
    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if (timeSinceLastShot >= shotInterval)
            {
                timeSinceLastShot = 0;
                Shoot();
            }
        }
    }
    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

    }
}
