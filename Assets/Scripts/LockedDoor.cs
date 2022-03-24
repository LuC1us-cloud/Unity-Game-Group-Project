using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private int count;  // Number of pressure plates with blocks on them
    public int numberOfPlates = 1;

    private void Awake()
    {
        count = 0;
    }

    // Method that 'opens' the locked door (right now destroys (idk how to spell it) it)
    public void Open()
    {
        count++;
        if (count == numberOfPlates) // Arbitrary number of plates, depends on what we want for the level
        {
            Destroy(gameObject);
        }
    }
}
