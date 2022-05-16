using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] startingRooms;
    public GameObject[] puzzleRooms;
    public GameObject[] enemyRooms;
    public GameObject[] bossRooms;
    public int maxRooms;
    [Range(-1, 1)]
    public float enemyRoomBias = 0f;

    void Start()
    {
        // normalize puzzleRoomBias
        // 0 = equal distribution of puzzle and enemy rooms, 1 = only puzzle rooms, -1 = only enemy rooms
        enemyRoomBias = enemyRoomBias + 1f;
        enemyRoomBias = enemyRoomBias / 2f;

        if (maxRooms > puzzleRooms.Length + enemyRooms.Length)
        {
            maxRooms = startingRooms.Length;
        }

        GameObject[] roomMatrix = new GameObject[maxRooms];
        for (int i = 0; i < maxRooms; i++)
        {
            roomMatrix[i] = GetRandomRoom();
            // spawn in that gameobject at the position of x=i, y=0
            // get that gameobject's Sprite renderer and set it's color to a random color
            // set the gameobject's tag to "Room"
            var reference = GameObject.Instantiate(roomMatrix[i], new Vector3(i, 0, 0), Quaternion.identity);
            reference.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

    }
    private GameObject GetRandomRoom()
    {

        int random = Random.Range(0, 100);
        if (random < enemyRoomBias * 100)
        {
            int randomIndex = Random.Range(0, puzzleRooms.Length);
            if (puzzleRooms[randomIndex] == null)
            {
                return GetRandomRoom();
            }
            GameObject room = puzzleRooms[randomIndex];
            puzzleRooms[randomIndex] = null;
            return room;
        }
        else
        {
            int randomIndex = Random.Range(0, enemyRooms.Length);
            if (enemyRooms[randomIndex] == null)
            {
                return GetRandomRoom();
            }
            GameObject room = enemyRooms[randomIndex];
            enemyRooms[randomIndex] = null;
            return room;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
