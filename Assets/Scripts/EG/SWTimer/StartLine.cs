using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : MonoBehaviour
{
    private Vector2 size;
    public Stopwatch stopwatch;
    private bool isActivated = false;

    public float time = 3f;
    public float timestamp = 0f;
    // Start is called before the first frame update
    void Start()
    {
        size = new Vector2(transform.localScale.x, transform.localScale.y);
        time = 3f;
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= timestamp + 3f){
            isActivated = false;
            ResetProgress();
        }
        
    }
    void ResetProgress(){
        if(!isActivated){
            Collider2D[] entities = Physics2D.OverlapBoxAll(transform.position, size, 0);
            foreach (Collider2D entity in entities)
            {
                if (entity.gameObject.tag == "Player")
                {
                    stopwatch.reset();
                    timestamp = time;
                    isActivated = true;
                }
            }
        }
    }
}
