using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour
{
    public float speed;
    public int Damage = 20;

    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyProjectile();
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<MainPlayer>().TakeDamage(Damage);
            DestroyProjectile();
        }
    }

    void DestroyProjectile(){
        Destroy(gameObject);
    }
}
