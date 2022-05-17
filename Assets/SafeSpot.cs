using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpot : MonoBehaviour
{
    private Collider2D[] colliders;

    public Transform pointA;
    public Transform pointB;

    [Min(0f)]
    public int healAmount = 10;

    private float waitTime;

    [Min(0f)]
    [SerializeField]
    private float setWaitTime;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(pointA.position.x, pointA.position.y, 0), new Vector3(pointA.position.x, pointB.position.y, 0));
        Gizmos.DrawLine(new Vector3(pointA.position.x, pointB.position.y, 0), new Vector3(pointB.position.x, pointB.position.y, 0));
        Gizmos.DrawLine(new Vector3(pointB.position.x, pointA.position.y, 0), new Vector3(pointB.position.x, pointB.position.y, 0));
        Gizmos.DrawLine(new Vector3(pointA.position.x, pointA.position.y, 0), new Vector3(pointB.position.x, pointA.position.y, 0));
    }

    private void FixedUpdate()
    {
        if (waitTime <= 0)
        {
            colliders = Physics2D.OverlapAreaAll(pointA.position, pointB.position);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Enemy")
                {
                    collider.GetComponent<Entity>().Heal(healAmount);
                }
            }
            waitTime = setWaitTime;
        }
        waitTime -= Time.deltaTime;

    }

}
