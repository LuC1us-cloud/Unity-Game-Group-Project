using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    [SerializeField]
    Transform nextPosition;
    bool playerDetected = false;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDetected)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                player.transform.position = nextPosition.position;
                playerDetected = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerDetected = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}
