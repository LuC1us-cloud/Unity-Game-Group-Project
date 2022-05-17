using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform target;
    public float speed = 3f;

	Vector2[] path;
	int targetIndex;

    public Transform[] patrolPoints;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine (RefreshPath ());
    }

    private void Update()
    {
        // colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        // foreach (Collider2D collider in colliders)
        // {
        //     if (collider.gameObject.tag == "Player")
        //     {
        //         //If the enemy found a player and isn't dashing, move towards the palyer 
        //         if (target != null && !isDashing)
        //         {
        //             target = collider.transform;
        //             LookAtPlayer(target);
        //         }

        //         //if dash cooldown is over and the palyer is in range for the charge, start charging phase
        //         if (dashCooldown <= 0 && Vector2.Distance(transform.position, target.position) < dashDistance)
        //         {
        //             target = transform;
        //             isDashing = true;
        //         }
                
        //         if (waitTime >= setWaitTime - 0.2) dashTarget = collider.transform.position;
        //         if (dashCooldown <= 0 && isDashing)
        //         {
        //             LookAtPlayer(dashTarget);
        //             waitTime -= Time.deltaTime;
        //         }
        //         if (dashTime >= 0 && waitTime <= 0 )
        //         {
        //             float step = dashSpeed * Time.deltaTime;
        //             transform.position = Vector2.MoveTowards(transform.position, dashTarget, step);
        //             dashTime -= Time.deltaTime;
        //         }
        //         if (dashTime <= 0  && waitTime <= 0 )
        //         {
        //             isDashing = false;
        //             dashCooldown = timeBetweenDashes;
        //             dashTime = setDashTime;
        //             waitTime = setWaitTime;
        //         }
        //         else
        //         {
        //             dashCooldown -= Time.deltaTime;
        //         }

        //     }
        // }
    }

    public void Move(Transform moveTarget)
	{
		target = moveTarget;
	}

    public void LookAtTarget(Transform target){
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle + 90;
        direction.Normalize();
    }

    public void LookAtTarget(Vector3 target){
        Vector3 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle + 90;
        direction.Normalize();
    }

    IEnumerator RefreshPath() {
		Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially
			
		while (true) {
			if (targetPositionOld != (Vector2)target.position) {
				targetPositionOld = (Vector2)target.position;

				path = Pathfinding.RequestPath (transform.position, target.position);
				StopCoroutine ("FollowPath");
				StartCoroutine ("FollowPath");
			}

			yield return new WaitForSeconds (.25f);
		}
	}
		
	IEnumerator FollowPath() {
		if (path.Length > 0) {
			targetIndex = 0;
			Vector2 currentWaypoint = path [0];

			while (true) {
				if ((Vector2)transform.position == currentWaypoint) {
					targetIndex++;
					if (targetIndex >= path.Length) {
						yield break;
					}
					currentWaypoint = path [targetIndex];
				}

				transform.position = Vector2.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
                LookAtTarget(currentWaypoint);
				yield return null;

			}
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				//Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}

}
