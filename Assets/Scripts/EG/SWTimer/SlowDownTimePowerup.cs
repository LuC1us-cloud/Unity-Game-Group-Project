using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownTimePowerup : MonoBehaviour
{
    private Vector2 size;
    public Stopwatch stopwatch;
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        size = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        SlowDownTime();
    }

    public void SlowDownTime(){
        Collider2D[] entities = Physics2D.OverlapBoxAll(transform.position, size, 0);
        foreach (Collider2D entity in entities)
        {
            if (entity.gameObject.tag == "Player")
            {
                stopwatch.timeCoef = 0.5f;
                Destroy(gameObject);
            }
        }
    }
}
