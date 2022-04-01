using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingArea : MonoBehaviour
{
    public float strength = 2f;
    private Vector2 size;
    private float damageTicker = 0;
    private float damageInterval = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        size = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        damageTicker += Time.deltaTime;
        DamagePlayer();
    }

    void DamagePlayer(){
        if (damageTicker >= damageInterval)
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
                    entity.GetComponent<Entity>().TakeDamage((int)(5f*strength));
                }
            }
            damageTicker = 0;
        }
    }
}
