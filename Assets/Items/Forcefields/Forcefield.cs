using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
public enum ForcefieldType
{
    Pushaway,
    Projectile_Shield,
    Damage
}
public class Forcefield : MonoBehaviour
{
    public Transform Sprite;
    public ForcefieldType type;
    [Header("Forcefield duration in seconds")]
    [Range(0, 10)]
    public float duration = 4f;
    [Header("Forcefield radius in Map Units")]
    [Range(0, 5)]
    public float radius = 2f;
    [Header("Affect multiplier")]
    [Range(0, 10)]
    public float strength = 1f;
    private float timeAlive = 0;
    private float damageTicker = 0;
    private float damageInterval = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        // get Circle Collider2D component and set radius
        Sprite.localScale = new Vector3(radius * 2, radius * 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= duration)
        {
            Destroy(gameObject);
        }
        damageTicker += Time.deltaTime;
        switch (type)
        {
            case ForcefieldType.Pushaway:
                PushAway();
                break;
            case ForcefieldType.Projectile_Shield:
                ProjectileShield();
                break;
            case ForcefieldType.Damage:
                Damage();
                break;
        }
    }
    void PushAway()
    {
        // get all enemies in radius
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius);
        // loop through all enemies
        foreach (Collider2D enemy in enemies)
        {
            // if enemy is an enemy
            if (enemy.gameObject.tag == "Enemy")
            {
                // get direction to enemy
                Vector2 direction = enemy.transform.position - transform.position;
                // get distance to enemy
                float distance = direction.magnitude;
                // get normalized direction
                direction = direction.normalized;
                // get force to apply
                Vector2 force = direction * (strength / distance);
                // apply force to enemy
                enemy.GetComponent<Rigidbody2D>().AddForce(force * Time.deltaTime * 3000 * strength);
            }
        }
    }
    void ProjectileShield()
    {
        // get all projectiles in radius
        Collider2D[] projectiles = Physics2D.OverlapCircleAll(transform.position, radius);
        // loop through all projectiles
        foreach (Collider2D projectile in projectiles)
        {
            // if projectile is a projectile
            if (projectile.gameObject.tag == "Projectile")
            {
                // destroy projectile
                Destroy(projectile.gameObject);
            }
        }
    }
    void Damage()
    {
        if (damageTicker >= damageInterval)
        {
            // get all enemies in radius
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius);
            // loop through all enemies
            foreach (Collider2D enemy in enemies)
            {
                // if enemy is an enemy
                if (enemy.gameObject.tag == "Enemy")
                {
                    // apply damage to enemy
                    enemy.GetComponent<Entity>().TakeDamage((int)(5f*strength));
                }
            }
            damageTicker = 0;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Forcefield))]
public class ForcefieldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
#endif