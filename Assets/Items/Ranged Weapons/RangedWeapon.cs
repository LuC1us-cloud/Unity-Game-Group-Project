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
    private float shootingTimer = 0;
    public void Shoot()
    {
        if (!canAttack) return;
        canAttack = false;

        foreach (Transform firePoint in firePoint)
        {
            GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Projectile>().SetProjectileLifeTime(attackRange);
            bullet.GetComponent<Projectile>().SetDamage(Random.Range(minDamage, maxDamage));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.up * projectileVelocity;
        }

    }
    private void Update()
    {
        if (!canAttack) shootingTimer += Time.deltaTime;
        if (shootingTimer >= 1 / attackSpeed)
        {
            canAttack = true;
            shootingTimer = 0;
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