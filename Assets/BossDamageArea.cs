using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageArea : MonoBehaviour
{
    private Vector2 size;
    
    // Start is called before the first frame update
    void Start()
    {
        size = new Vector2(transform.localScale.x, transform.localScale.y);
    }
    public void DamagePlayer(int damage)
    {
        // get all enemies in radius
        Collider2D[] entities = Physics2D.OverlapBoxAll(transform.position, size, 0);
        // loop through all enemies
        foreach (Collider2D entity in entities)
        {
            // if enemy is an enemy
            if (entity.gameObject.tag == "Player")
            {
                // apply damage to enemy
                entity.GetComponent<Entity>().TakeDamage(damage);
            }
        }

    }
}
