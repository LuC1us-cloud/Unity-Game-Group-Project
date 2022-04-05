using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public GameObject boss;
    private float timeBtwShots;
    public float startTimeBtwShots;

    private Entity entity;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        entity = boss.GetComponent<Entity>(); //Might need for later
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
