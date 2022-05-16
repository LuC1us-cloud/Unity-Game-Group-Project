using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    List<Vector3> positions;
    List<GameObject> boxes;
    List<PressurePlate> plates;
    List<LockedDoor> doors;
    
    void Start()
    {
        var player = FindObjectOfType<MainPlayer>();
        Vector3 position = player.transform.position;
        var allBoxes = GameObject.FindGameObjectsWithTag("Movable");
        var allDoors = FindObjectsOfType<LockedDoor>();
        var allPlates = FindObjectsOfType<PressurePlate>();
        positions = new List<Vector3>();
        boxes = new List<GameObject>();
        plates = new List<PressurePlate>();
        doors = new List<LockedDoor>();
        foreach (GameObject box in allBoxes)
        {
            float dist = (box.transform.position - position).sqrMagnitude;
         Debug.Log(dist);
            if (dist < 150)
            {
                positions.Add(box.transform.position);
                boxes.Add(box);
            }
        }
        foreach (LockedDoor door in allDoors)
        {
            float dist = (door.transform.position - position).sqrMagnitude;
            if (dist < 150)
            {
                doors.Add(door);
            }
        }
        foreach (PressurePlate plate in allPlates)
        {
            float dist = (plate.transform.position - position).sqrMagnitude;
            if (dist < 150)
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
