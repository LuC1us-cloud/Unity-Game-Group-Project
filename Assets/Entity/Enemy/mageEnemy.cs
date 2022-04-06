using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageEnemy : MonoBehaviour
{
    public float speed;

    public GameObject damageText;
    private Rigidbody2D rb;

    public int CurrentHealth = 100;
    public int MaxHealth = 100;
    public int Armor = 10;
    public float detectionRadius = 10;
    public float stoppingDistance;
    public float retreaDistance;
    
    private float timeBtwShots;
    public float startTimeBtwShots;

    private Transform player;
    public GameObject projectile;

    private Collider2D[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        colliders = Physics2D.OverlapCircleAll (transform.position, detectionRadius);
        foreach(Collider2D collider in colliders){
            if(collider.gameObject.tag == "Player"){
                player = collider.transform;

                LookAtPlayer(player);

                if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                } 
                else if (Vector2.Distance(transform.position, player.position) > retreaDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                }

                if(timeBtwShots <= 0){
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
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

    private void LookAtPlayer(Transform player){
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
    }
}
