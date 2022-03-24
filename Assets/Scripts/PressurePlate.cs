using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    // Check collision and change color and stop movement if
    // object has the 'Movable' tag and is close enough
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Movable")) return;
        // Check how close the block must be to the plate
        // As in rn it has to be right on top of the plate with very minimal space not covered by the block
        if (Vector2.Distance(transform.position, other.transform.position) > 0.2f) return;
        Rigidbody2D rb = other.attachedRigidbody;
        //if (!(rb is null))
        //{
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            //gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.red);
        LockedDoor door = FindObjectOfType<LockedDoor>();
        Destroy(this);
        door.Open();    // 'Opens' the door (rn just removes the object)
        //}
    }
}
