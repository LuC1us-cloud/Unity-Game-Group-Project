using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : Interactable
{
    [SerializeField]
    Transform nextPosition;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // Get mainplayer gameobject
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Interact()
    {
        player.transform.position = nextPosition.position;
        if (gameObject.tag == "Exit")
        {
            var enterPoints = GameObject.FindGameObjectsWithTag("Enter");
            foreach (var enter in enterPoints)
            {
                Destroy(enter);
            }
        }
    }
}
