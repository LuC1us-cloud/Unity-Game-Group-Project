using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Interactable
{
    public override void Interact()
    {
        // change the color of the game object to green once the player interacts with it
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;
    }
}
