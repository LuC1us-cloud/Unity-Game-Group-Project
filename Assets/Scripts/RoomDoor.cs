using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType { Other, EntryDoor, ExitDoor }
public class RoomDoor : Interactable
{
    private Transform nextPosition;
    public DoorType doorType;
    public string id;
    public string targetDoorId;
    GameObject player;
    public void Initialize()
    {
        // Get mainplayer gameobject
        player = GameObject.FindGameObjectWithTag("Player");
        // find all gameobjects with tag Door
        var doors = GameObject.FindGameObjectsWithTag("Door");
        // find the door with the same id as targetDoorId
        RoomDoor targetDoor = null;
        foreach (GameObject door in doors)
        {
            var roomDoor = door.GetComponent<RoomDoor>();
            if (roomDoor.id == this.id) continue;

            if (roomDoor.id == targetDoorId)
            {
                targetDoor = roomDoor;
                break;
            }
        }
        // set nextPosition to the target door's transform
        nextPosition = targetDoor.transform;
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
