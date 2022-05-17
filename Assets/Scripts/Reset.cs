using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject topRight;
    public GameObject bottomLeft;
    List<Vector3> positions;
    List<GameObject> boxes;
    List<PressurePlate> plates;
    List<LockedDoor> doors;
    
    void Start()
    {
        var allBoxes = GameObject.FindGameObjectsWithTag("Movable");
        var allDoors = FindObjectsOfType<LockedDoor>();
        var allPlates = FindObjectsOfType<PressurePlate>();

        positions = new List<Vector3>();
        boxes = new List<GameObject>();
        plates = new List<PressurePlate>();
        doors = new List<LockedDoor>();

        var tprx = topRight.transform.position.x;
        var tpry = topRight.transform.position.y;
        var btlx = bottomLeft.transform.position.x;
        var btly = bottomLeft.transform.position.y;
        foreach (GameObject box in allBoxes)
        {
            var x = box.transform.position.x;
            var y = box.transform.position.y;
            if (x <= tprx && x >= btlx && y <= tpry && y >= btly)
            {
                Debug.Log(box.transform.position);
                positions.Add(box.transform.position);
                boxes.Add(box);
            }
        }
        foreach (LockedDoor door in allDoors)
        {
            var x = door.transform.position.x;
            var y = door.transform.position.y;
            if (x <= tprx && x >= btlx && y <= tpry && y >= btly)
            {
                doors.Add(door);
            }
        }
        foreach (PressurePlate plate in allPlates)
        {
            var x = plate.transform.position.x;
            var y = plate.transform.position.y;
            if (x <= tprx && x >= btlx && y <= tpry && y >= btly)
            {
                plates.Add(plate);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < boxes.Count; i++)
            {
                boxes[i].transform.position = positions[i];
                boxes[i].GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePosition;
            }
            foreach (LockedDoor door in doors)
            {
                door.Reset();
            }
            foreach (PressurePlate plate in plates)
            {
                var renderer = plate.GetComponent<Renderer>();
                renderer.material.SetColor("_Color", Color.yellow);
                plate.Enable();
            }
        }
    }
}
