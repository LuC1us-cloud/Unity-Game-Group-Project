using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovementTowardsPoint : MonoBehaviour
{
    private Timer timer;
    private Vector2 startingPoint;
    [SerializeField] private GameObject point;
    [SerializeField] private float speed = 5f;
    private bool isAtMiddle = false;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
        timer = gameObject.AddComponent<Timer>();
        timer.Initialize(1.5f);
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAtMiddle){
            transform.position = Vector2.MoveTowards(transform.position, startingPoint, speed * Time.deltaTime);
        }
        else{
            transform.position = Vector2.MoveTowards(transform.position, point.transform.position, speed * Time.deltaTime);
        }
        if (timer.isDone) {
            timer.Rerun();
            isAtMiddle = !isAtMiddle;
        }
    }
}
