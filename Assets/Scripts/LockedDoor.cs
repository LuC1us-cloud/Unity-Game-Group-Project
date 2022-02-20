using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private Collider collider;  // Used only if we choose not to destroy the door
    private int count;  // Number of pressure plates with blocks on them

    private void Awake()
    {
        collider = GetComponent<Collider>();
        count = 0;
    }

    // Method that 'opens' the locked door (right now destroys (idk how to spell it) it)
    public void Open()
    {
        count++;
        if (count == 5) // Arbitrary number of plates, depends on what we want for the level
        {
            collider.enabled = false;   // Kept it in but this would be used for the
                                        // 'not destroy the door' option for the level
            Destroy(gameObject);
        }
    }
}
