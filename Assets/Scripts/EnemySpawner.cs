using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{ 
    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave 
    {
        public string name;
        public Enemy[] enemies;
        public float rate;
    }

    [System.Serializable]
    public class Enemy 
    {
        public Transform enemy;
        public Transform spawnPoint;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    private SpawnState state = SpawnState.COUNTING;
    private float searchCountdown = 1f;

    private void Start() 
    {
        
        waveCountdown = timeBetweenWaves;

    }

    private void Update() 
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

    }

    //script for displaying or doing something after all waves have been completed
    void WaveCompleted()
    {
        Debug.Log("Wave completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
         if (nextWave + 1 > waves.Length - 1)
         {
             Debug.Log("Completed all waves!");
             this.gameObject.SetActive(false);  
         }
         else
         {
            nextWave++;
         }
    }

    // script for checking if there are enemies left before spawning the next wave
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        { 
            searchCountdown = 1f;
            //the checking function can and should be changed for something suitable
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }

        }
        return true;
    }
    
    // coroutine for spawning enemies in fixed time intervals
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.enemies.Length; i++)
        {
            SpawnEnemy(_wave.enemies[i]);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;

    }

    //scrip for insanciating the enemy object
    void SpawnEnemy(Enemy _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.enemy.name);
        Instantiate(_enemy.enemy, _enemy.spawnPoint.position, _enemy.spawnPoint.rotation);
    }

}
