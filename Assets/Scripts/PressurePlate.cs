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
        if (Vector2.Distance(transform.position, other.transform.position) > 0.1f) return;
        Rigidbody2D rb = other.attachedRigidbody;
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.red);
        var player = FindObjectOfType<MainPlayer>();
        Vector3 position = player.transform.position;
        var doors = FindObjectsOfType<LockedDoor>();
        foreach (LockedDoor door in doors)
        {
            float dist = (door.transform.position - position).sqrMagnitude;
            if (dist < 100)
            {
                door.Open();    // 'Opens' the door (rn just removes the object)
            }
        }
        Destroy(this);
    }
}
