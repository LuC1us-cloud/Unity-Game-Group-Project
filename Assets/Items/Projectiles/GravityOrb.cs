using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrb : MonoBehaviour
{
    public Transform Sprite;
    [Header("Orb duration in seconds")]
    [Range(0, 10)]
    public float duration = 4f;
    [Header("Orb radius in Map Units")]
    [Range(0, 5)]
    public float radius = 2f;
    [Header("Affect multiplier")]
    [Range(0, 10)]
    public float strength = 1f;
    private float timeAlive = 0;
    private float damageTicker = 0;
    private float damageInterval = 0.25f;
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
        PushInto();
    }
    void PushInto()
    {
        // get all enemies in radius
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius);
        // loop through all enemies
        foreach (Collider2D enemy in enemies)
        {
            // if enemy is an enemy
            if (enemy.gameObject.tag == "Enemy" && enemy.GetComponent<Rigidbody2D>() != null)
            {
                // get direction to enemy
                Vector2 direction = enemy.transform.position - transform.position;
                // get distance to enemy
                float distance = direction.magnitude;
                // get normalized direction
                direction = direction.normalized;
                // reverse the direction
                direction = -direction;
                // get force to apply
                Vector2 force = direction * (strength / distance);
                // apply force to enemy
                enemy.GetComponent<Rigidbody2D>().AddForce(force * Time.deltaTime * 3000 * strength);
            }
        }
    }
}
