using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    // Once the button from the OpenDoor.cs script is pressed, the door will move.
    // This is done by changing the position of the door.
    public float speed = 1.0f;
    public float distance = 1.0f;
    public GameObject button;

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * distance;
    }

    void Update()
    {
        // if the button is pressed, move the door
        if (button.GetComponent<SpriteRenderer>().material.color == Color.green)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
        }
    }
}
