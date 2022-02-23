using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainPlayer : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float detectionRadius = 10;
    
    private Collider2D[] colliders;

    private void Update(){
        colliders = Physics2D.OverlapCircleAll (transform.position, detectionRadius);
        foreach(Collider2D collider in colliders){
            if(collider.gameObject.tag == "Player"){
                target = collider.transform;
                if(target != null){
                    float step = speed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, target.position, step);
                }
            }
        }
    }
}
