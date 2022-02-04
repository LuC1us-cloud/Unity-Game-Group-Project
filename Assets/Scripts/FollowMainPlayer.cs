using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainPlayer : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private void Update(){
        if(target != null){
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player"){
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       if(other.gameObject.tag == "Player"){
            target = null;
        }
    }

}
