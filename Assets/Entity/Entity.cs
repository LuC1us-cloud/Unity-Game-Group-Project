using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject damageText;
    public float speed = 3f;
    public int CurrentHealth = 100;
    public int MaxHealth = 100;
    public int Damage = 10;
    public int Armor = 10;
    public int PassiveHealingInterval = 5;
    public int PassiveHealingAmount = 10;
    private float timeSinceLastHeal = 0;
    public bool isInvincible = false;
    public bool isStunned = false;
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
        if(isInvincible) return;
        // damage can't be less than 1
        int damageToTake = Mathf.Max(1, damage - Armor);
        CurrentHealth -= damageToTake;
        DamageIndicator damageIndicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        damageIndicator.SetDamageText(damageToTake);
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
        if(isStunned) return;
        // get rigidbody
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) return;
        // move to position
        rb.MovePosition(position);
    }
}
