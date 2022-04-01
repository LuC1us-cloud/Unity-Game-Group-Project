using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerTrap : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public LayerMask layers;
    public int damage = 10;
    public float flameDuration = 2f;
    public float flameCooldown = 3f;
    public float damageInterval = 0.25f;
    private float flameTimer = 0f;
    private float cooldownTimer = 0f;
    private float damageTimer = 0f;

    private void FixedUpdate()
    {
        // if bullet is alive for too long, destroy it
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= flameCooldown)
        {
            flameTimer += Time.deltaTime;

            if (flameTimer <= flameDuration)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer >= damageInterval)
                {
                    this.GetComponent<ParticleSystem>().Play();
                    Collider2D hit = Physics2D.OverlapArea(pointA.transform.position, pointB.transform.position, layers);
                    if (hit != null)
                    {
                        hit.gameObject.GetComponent<MainPlayer>().TakeDamage(damage);
                    } 
                    damageTimer = 0f;
                }
            }
            else
            {
                cooldownTimer = 0f;
                flameTimer = 0f;
            }
        }
    }
}
