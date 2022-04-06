using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform target;
    private Vector3 dashTarget;
    public float speed = 3f;
    public float detectionRadius = 10;
    private Collider2D[] colliders;
    private float dashCooldown;
    public float timeBetweenDashes;
    public float dashSpeed;
    public float dashDistance;
    private bool isDashing;
    private float dashTime;
    public float setDashTime;
    private float waitTime;
    public float setWaitTime;

    void Start()
    {
        dashCooldown = timeBetweenDashes;
        dashTime = setDashTime;
        waitTime = setWaitTime;
        rb = this.GetComponent<Rigidbody2D>();
        isDashing = false;
    }

    private void Update()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                target = collider.transform;

                //If the enemy found a player and isn't dashing, move towards the palyer 
                if (target != null && !isDashing)
                {
                    LookAtPlayer(target);
                    float step = speed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, target.position, step);
                }

                //if dash cooldown is over and the palyer is in range for the charge, start charging phase
                if (dashCooldown <= 0 && Vector2.Distance(transform.position, target.position) < dashDistance)
                {
                    isDashing = true;
                }
                
                if (waitTime >= setWaitTime - 0.2) dashTarget = collider.transform.position;
                if (dashCooldown <= 0 && isDashing)
                {
                    LookAtPlayer(dashTarget);
                    waitTime -= Time.deltaTime;
                }
                if (dashTime >= 0 && waitTime <= 0 )
                {
                    float step = dashSpeed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, dashTarget, step);
                    dashTime -= Time.deltaTime;
                }
                if (dashTime <= 0  && waitTime <= 0 )
                {
                    isDashing = false;
                    dashCooldown = timeBetweenDashes;
                    dashTime = setDashTime;
                    waitTime = setWaitTime;
                }
                else
                {
                    dashCooldown -= Time.deltaTime;
                }

            }
        }
    }

    private void LookAtPlayer(Transform player){
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle + 90;
        direction.Normalize();
    }

    private void LookAtPlayer(Vector3 player){
        Vector3 direction = player - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle + 90;
        direction.Normalize();
    }

}
