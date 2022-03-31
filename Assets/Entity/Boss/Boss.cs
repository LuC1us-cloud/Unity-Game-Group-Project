using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float Speed;
    public bool isPatroling;
    private bool flip;

    public Rigidbody2D rb;
    public Transform wallCheckPos;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        isPatroling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPatroling){
            Patrol();
            flip = Physics2D.OverlapCircle(wallCheckPos.position, 0.1f, groundLayer);
        }
    }

    //private void FixedUpdate(){
     //   if(isPatroling){
    //        flip = Physics2D.OverlapCircle(wallCheckPos.position, 0.1f, groundLayer);
    //    }
  //  }

    void Patrol(){
        if(flip){
            Flip();
        }
        rb.velocity = new Vector2(Speed * Time.fixedDeltaTime, rb.velocity.y);
    }
    void Flip(){
        isPatroling = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        Speed *= -1;
        isPatroling = true;
    }

}
