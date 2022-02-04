using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public float speed = 3f;
    public Transform target;
    public int CurrentHealth = 100;
    public int MaxHealth = 100;
    public int Damage = 10;
    public int Armor = 10;
    public int PassiveHealingInterval = 5;
    public int PassiveHealingAmount = 10;
    private float timeSinceLastHeal = 0;

    private void Update(){
        if(target != null){
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player"){
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       if(other.gameObject.tag == "Player"){
            target = null;
        }
    }


    private void FixedUpdate()
    {
        // if health reaches 0, destroy the object
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

        // if health is below max health, heal health every time interval
        if (CurrentHealth < MaxHealth)
        {
            timeSinceLastHeal += Time.deltaTime;
            if (timeSinceLastHeal >= PassiveHealingInterval)
            {
                CurrentHealth += PassiveHealingAmount;
                timeSinceLastHeal = 0;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        // damage can't be less than 1
        CurrentHealth -= Mathf.Max(1, damage - Armor);
    }

    public void Heal(int heal)
    {
        CurrentHealth += heal;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void MoveTo(Vector2 position)
    {
        // get rigidbody
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb == null) return;
        // move to position
        rb.MovePosition(position);
    }
}
