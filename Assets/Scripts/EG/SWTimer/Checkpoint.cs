using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector2 size;
    public Stopwatch stopwatch;
    private bool isActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        size = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        SetTime();
    }

    void SetTime(){
        if(!isActivated){
            Collider2D[] entities = Physics2D.OverlapBoxAll(transform.position, size, 0);
            foreach (Collider2D entity in entities)
            {
                if (entity.gameObject.tag == "Player")
                {
                    stopwatch.lap();
                    isActivated = true;
                }
            }
        }
    }
}