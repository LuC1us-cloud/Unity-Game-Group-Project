using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public GameObject EnemySpawner;
    private Collider2D[] colliders;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            colliders = Physics2D.OverlapAreaAll(pointA.position, pointB.position);
            foreach(Collider2D collider in colliders)
            {
                if(collider.gameObject.tag == "Enemy")
                {
                    Destroy(collider.gameObject);
                }
            }
            EnemySpawner.SetActive(false);
        }
    }
}
