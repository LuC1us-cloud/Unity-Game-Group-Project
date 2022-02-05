using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RangedWeapon : Item
{
    public GameObject projectile;
    public Transform[] firePoint;
    public int minDamage;
    public int maxDamage;
    [Header("Attacks per second")]
    public float attackSpeed;
    [Header("Seconds that the bullet will be alive")]
    public float attackRange;
    public float projectileVelocity;
    // [Header("1 is fully accurate, 0.01 is fully inaccurate")]
    // public float accuracy;
    // public float attackDelay;
    // public float attackKnockbackDuration;
    // public float attackKnockbackDistance;
    private bool canAttack = true;
    private float timeSinceLastAttack = 0;
    public void Shoot()
    {
        if (canAttack)
        {
            canAttack = false;
            foreach(Transform firePoint in firePoint)
            {
                GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
                bullet.GetComponent<Projectile>().SetProjectileLifeTime(attackRange);
                bullet.GetComponent<Projectile>().SetDamage(Random.Range(minDamage, maxDamage));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = firePoint.up * projectileVelocity;
            }
            // var damage = Random.Range(minDamage, maxDamage);
            // var bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
            // bullet.GetComponent<Projectile>().SetDamage(damage);
            // var rb = bullet.GetComponent<Rigidbody2D>();
            // rb.AddForce(firePoint.up * projectileVelocity, ForceMode2D.Impulse);
        }
    }
    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack >= 1 / attackSpeed)
        {
            canAttack = true;
            timeSinceLastAttack = 0;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(RangedWeapon))]
public class RangedWeaponEditor : ItemEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif