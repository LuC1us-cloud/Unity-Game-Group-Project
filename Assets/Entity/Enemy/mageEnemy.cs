using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageEnemy : MonoBehaviour
{
    public bool isElite = false;

    private Rigidbody2D rb;
    public float speed = 10;

    public GameObject projectile;

    public Transform target;

    private GameObject projectileTemp;

    Vector2[] path;
    int targetIndex;

    public Transform[] patrolPoints;

    public Transform safeSpot;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(RefreshPath());
    }

    // Update is called once per frame
    void Update()
    {
        // colliders = Physics2D.OverlapCircleAll (transform.position, detectionRadius);
        // foreach(Collider2D collider in colliders){
        //     if(collider.gameObject.tag == "Player"){
        //         player = collider.transform;

        //         LookAtPlayer(player);

        //         if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
        //         {
        //             target = player;
        //         } 
        //         else if (Vector2.Distance(transform.position, player.position) > retreaDistance)
        //         {
        //             target = transform;
        //             transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        //         }

        //         if(timeBtwShots <= 0){
        //             projectileTemp = Instantiate(projectile, transform.position, Quaternion.identity);
        //             projectileTemp.GetComponent<fireBall>().target = player.position;
        //             projectileTemp = Instantiate(projectile, transform.position, Quaternion.identity);
        //             projectileTemp.GetComponent<fireBall>().target.x = player.position.x;
        //             projectileTemp.GetComponent<fireBall>().target.y = player.position.y + 2;
        //             projectileTemp = Instantiate(projectile, transform.position, Quaternion.identity);
        //             projectileTemp.GetComponent<fireBall>().target.x = player.position.x;
        //             projectileTemp.GetComponent<fireBall>().target.y = player.position.y - 2;
        //             timeBtwShots = startTimeBtwShots;
        //         }
        //         else
        //         {
        //             timeBtwShots -= Time.deltaTime;
        //         }
        //     }
        // }

    }

    public void Move(Transform moveTarget)
    {
        target = moveTarget;
    }

    public void Fire(Transform target)
    {
        if (isElite)
        {
            projectileTemp = Instantiate(projectile, transform.position, Quaternion.identity);
            projectileTemp.GetComponent<fireBall>().target = target.position;
            projectileTemp = Instantiate(projectile, transform.position, Quaternion.identity);
            projectileTemp.GetComponent<fireBall>().target.x = target.position.x;
            projectileTemp.GetComponent<fireBall>().target.y = target.position.y + 2;
            projectileTemp = Instantiate(projectile, transform.position, Quaternion.identity);
            projectileTemp.GetComponent<fireBall>().target.x = target.position.x;
            projectileTemp.GetComponent<fireBall>().target.y = target.position.y - 2;
        }
        else
        {
            projectileTemp = Instantiate(projectile, transform.position, Quaternion.identity);
            projectileTemp.GetComponent<fireBall>().target = target.position;
        }
    }

    private void LookAtTarget(Transform player)
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
    }

    public void LookAtTarget(Vector3 target){
        Vector3 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle + 90;
        direction.Normalize();
    }

    IEnumerator RefreshPath()
    {
        Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially

        while (true)
        {
            if (targetPositionOld != (Vector2)target.position)
            {
                targetPositionOld = (Vector2)target.position;

                path = Pathfinding.RequestPath(transform.position, target.position);
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }

            yield return new WaitForSeconds(.25f);
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            targetIndex = 0;
            Vector2 currentWaypoint = path[0];

            while (true)
            {
                if ((Vector2)transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }
                LookAtTarget(currentWaypoint);
                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;

            }
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                //Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

}
