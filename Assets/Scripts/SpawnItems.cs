using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class SpawnItems : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<Transform> items;

    void Start()
    {
        Random rnd = new Random();
        foreach (var spawnPoint in spawnPoints)
        {
            int index = rnd.Next(items.Count);
            Instantiate(items[index], spawnPoint.position, spawnPoint.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
