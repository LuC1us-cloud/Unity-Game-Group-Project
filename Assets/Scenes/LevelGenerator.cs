using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> startingRooms;
    public List<GameObject> puzzleRooms;
    public List<GameObject> enemyRooms;
    public List<GameObject> bossRooms;
    public int maxRooms;
    [Range(-1, 1)]
    public float enemyRoomBias = 0f;
    public float cursorPositionX = 0f;
    public float largestRoomY = 0f;
    private string lastDoorId = "123";
    private string nextDoorId = "asdfghjklqwetyuipzxcvbmn1";

    void Awake()
    {

        // normalize puzzleRoomBias
        // 0 = equal distribution of puzzle and enemy rooms, 1 = only puzzle rooms, -1 = only enemy rooms
        enemyRoomBias = enemyRoomBias + 1f;
        enemyRoomBias = enemyRoomBias / 2f;

        if (maxRooms > puzzleRooms.Count + enemyRooms.Count)
        {
            maxRooms = puzzleRooms.Count + enemyRooms.Count;
        }

        GameObject[] roomMatrix = new GameObject[maxRooms];
        for (int i = 0; i < maxRooms; i++)
        {
            var currentRoom = GetRandomRoom();
            // spawn in that gameobject at the position of x=i, y=0
            // get that gameobject's Sprite renderer and set it's color to a random color
            // set the gameobject's tag to "Room"
            var reference = GameObject.Instantiate(currentRoom, new Vector3(cursorPositionX, 0, 0), Quaternion.identity);
            SetDoorConnections(reference);
            roomMatrix[i] = reference;

            var roomWidth = reference.GetComponent<SpriteRenderer>().bounds.size.x;
            if (reference.GetComponent<SpriteRenderer>().bounds.size.y > largestRoomY)
            {
                largestRoomY = reference.GetComponent<SpriteRenderer>().bounds.size.y;
            }
            // reference.transform.position = new Vector3(cursorPositionX + roomWidth / 2, 0, 0);
            cursorPositionX += roomWidth * 2;
            // disable the gameobjects sprire renderer
            reference.GetComponent<SpriteRenderer>().enabled = false;
        }
        // foreach (var room in roomMatrix)
        // {
        //     // move the rooms x position to the left by half of cursor position x
        //     room.transform.position = new Vector3(room.transform.position.x - cursorPositionX / 2, room.transform.position.y, room.transform.position.z);
        // }
    }
    void Start()
    {
        InitializePathfinding();
        InitializeAllDoors();
    }
    private void InitializePathfinding()
    {
        // find a gameobject with tag "Pathfinding"
        // get its component Grid and initialize it
        // then get its component Pathfinding and initialize it
        var pathfinder = GameObject.FindGameObjectWithTag("Pathfinding");

        var grid = pathfinder.GetComponent<Grid>();
        grid.gridWorldSize = new Vector2(cursorPositionX * 2, largestRoomY);
        grid.Initialize();
        var pathfinding = pathfinder.GetComponent<Grid>();
        pathfinding.Initialize();
    }
    private void SetDoorConnections(GameObject room)
    {
        // get all the gameobjects with RoomDoor script on them
        var doors = room.GetComponentsInChildren<RoomDoor>();
        if (doors == null) return;
        // for each of those gameobjects
        foreach (var door in doors)
        {
            // if the door DoorType is Entry Door, then set the door's Id to lastDoorId
            if (door.doorType == DoorType.EntryDoor)
            {
                door.id = nextDoorId;
                door.targetDoorId = lastDoorId;
                lastDoorId = RandomString();
                nextDoorId = RandomString();
            }
        }
        foreach (var door in doors)
        {
            // if the door DoorType is Entry Door, then set the door's Id to lastDoorId
            if (door.doorType == DoorType.ExitDoor)
            {
                door.id = lastDoorId;
                door.targetDoorId = nextDoorId;
            }
        }
    }
    private void InitializeAllDoors()
    {
        // find all gameobjects with tag "Door" get their RoomDoor script and call the Initialize method
        var doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (var door in doors)
        {
            door.GetComponent<RoomDoor>().Initialize();
        }
    }
    private string RandomString()
    {
        // return a random string of length 16, with numbers and characters
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        string randomString = "";
        for (int i = 0; i < 16; i++)
        {
            int randomIndex = Random.Range(0, chars.Length);
            randomString += chars[randomIndex];
        }
        return randomString;
    }
    private GameObject GetRandomRoom()
    {
        int random = Random.Range(0, 100);
        var roomList = enemyRooms;
        if (random < enemyRoomBias * 100)
        {
            roomList = puzzleRooms;
        }
        if (enemyRooms.Count == 0)
        {
            roomList = puzzleRooms;
        }
        if (puzzleRooms.Count == 0)
        {
            roomList = enemyRooms;
        }

        int randomIndex = Random.Range(0, roomList.Count);
        // Take the random room from the list and remove it from the list, then return it
        var room = roomList[randomIndex];
        roomList.RemoveAt(randomIndex);
        return room;
    }
}
