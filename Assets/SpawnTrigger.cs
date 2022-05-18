using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    
    public GameObject EnemySpawner;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnemySpawner.GetComponent<EnemySpawner>().enabled = true;
            EnemySpawner.GetComponent<EnemySpawner>().Initialize(); 
            this.gameObject.SetActive(false);
        }
    }

}
