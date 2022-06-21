using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFasterPowerup : MonoBehaviour
{
    private Vector2 size;
    public PlayerMovement playerMovement;
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        size = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        MoveFaster();
    }
    public void MoveFaster(){
        Collider2D[] entities = Physics2D.OverlapBoxAll(transform.position, size, 0);
        foreach (Collider2D entity in entities)
        {
            if (entity.gameObject.tag == "Player")
            {
                playerMovement.moveSpeedCoef = 1.75f;
                Destroy(gameObject);
            }
        }
    }
}
