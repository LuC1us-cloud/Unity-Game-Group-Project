using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    // Check collision and change color and stop movement if
    // object has the 'Movable' tag and is close enough
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Movable")) return;
        // Check how close the block must be to the plate
        // As in rn it has to be right on top of the plate with very minimal space not covered by the block
        if (Vector3.Distance(transform.position, other.transform.position) > 0.1f) return;
        Rigidbody rb = other.attachedRigidbody;
        if (!(rb is null))
        {
            other.attachedRigidbody.isKinematic = true;
            gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
            LockedDoor door = FindObjectOfType<LockedDoor>();
            Destroy(this);
            door.Open();    // 'Opens' the door (rn just removes the object)
        }
    }
}
