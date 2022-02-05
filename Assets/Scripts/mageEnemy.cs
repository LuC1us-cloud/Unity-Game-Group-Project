using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageEnemy : MonoBehaviour
{
    public float speed;

    public GameObject damageText;
    public int CurrentHealth = 100;
    public int MaxHealth = 100;
    public int Damage = 10;
    public int Armor = 10;
    public int PassiveHealingInterval = 5;
    public int PassiveHealingAmount = 10;
    private float timeSinceLastHeal = 0;

    public float stoppingDistance;
    public float retreaDistance;
    
    private float timeBtwShots;
    public float startTimeBtwShots;

    public Transform player;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
         timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            } else if(Vector2.Distance(transform.position, player.position) > stoppingDistance && Vector2.Distance(transform.position, player.position) > retreaDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) > retreaDistance)
            {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            if (CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }

            if(timeBtwShots <= 0){
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }else{
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int damage)
    {
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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player"){
            player = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       if(other.gameObject.tag == "Player"){
            player = null;
        }
    }
}
