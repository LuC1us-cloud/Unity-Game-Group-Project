using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType { Other, EntryDoor, ExitDoor }
public class RoomDoor : Interactable
{
    [SerializeField]
    Transform nextPosition;
    public DoorType doorType;
    public string id;
    public string targetDoorId;
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
