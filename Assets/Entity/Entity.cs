using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject deathAnim;
    public GameObject damageText;
    public float speed = 3f;
    public int damageInterval = 1;
    public int CurrentHealth = 100;
    public int MaxHealth = 100;
    public int Damage = 5;
    public int Armor = 10;
    public int PassiveHealingInterval = 5;
    public int PassiveHealingAmount = 10;
    private float timeSinceLastHeal = 0;
    private float timeSinceLastDamge = 0;
    public bool isInvincible = false;
    public bool isStunned = false;
    private GameObject highscoreRef;
    private void FixedUpdate()
    {
        // if health reaches 0, destroy the object
        if (CurrentHealth <= 0)
        {
            Die();
        }

        // if health is below max health, heal health every time interval
        if (CurrentHealth < MaxHealth)
        {
            timeSinceLastHeal += Time.deltaTime;
            if (timeSinceLastHeal >= PassiveHealingInterval)
            {
                CurrentHealth += PassiveHealingAmount;
                // if current health is more than max correct it
                if (CurrentHealth > MaxHealth)
                {
                    CurrentHealth = MaxHealth;
                }
                timeSinceLastHeal = 0;
            }
        }
        if (timeSinceLastDamge <= damageInterval)
        {
            timeSinceLastDamge += Time.deltaTime;
        }
    }
    private void Start() {
        highscoreRef = GameObject.FindGameObjectWithTag("Highscore");
    }
    /// <summary>
    /// Take damage from a source
    /// </summary>
    /// <param name="damage">The amount of damage to take</param>
    /// <param name="hitPoint">Location to spawn the damage text. <para/> Default: transform.position</param>
    public void TakeDamage(int damage, Vector3 hitPoint = new Vector3())
    {
        if (hitPoint == new Vector3())
        {
            hitPoint = transform.position;
        }
        if (isInvincible) return;
        // damage can't be less than 1
        int damageToTake = Mathf.Max(1, damage - Armor);
        CurrentHealth -= damageToTake;
        DamageIndicator damageIndicator = Instantiate(damageText, hitPoint, Quaternion.identity).GetComponent<DamageIndicator>();
        damageIndicator.SetDamageText(damageToTake);
    }
    public void Die()
    {
            var scoreObject = GameObject.FindGameObjectWithTag("ScoreText");
            scoreObject.GetComponent<Score>().AddScore(30);
            if(gameObject.name == "Boss (1)"){
                
            scoreObject.GetComponent<Score>().AddScore(500);
            highscoreRef.GetComponent<Highscore>().DisplayScores();
            }
        Destroy(gameObject);
        Instantiate(deathAnim, transform.position, Quaternion.identity);
        Debug.Log(deathAnim.name);
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
        if (isStunned) return;
        // get rigidbody
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) return;
        // move to position
        rb.MovePosition(position);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && timeSinceLastDamge >= damageInterval)
        {
            other.gameObject.GetComponent<MainPlayer>().TakeDamage(Damage);
            timeSinceLastDamge = 0;
        }
    }
}
