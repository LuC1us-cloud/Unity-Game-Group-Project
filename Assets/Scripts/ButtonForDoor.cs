using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForDoor : Interactable
{
    // create a timer
    public float timer = 0.0f;
    public override void Interact()
    {
        // change the color of the game object to green once the player interacts with it
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;
    }
    // when 3 seconds pass, the button changes colors to original
    void Update()
    {
        if (gameObject.GetComponent<SpriteRenderer>().material.color == Color.green)
        {
            timer += Time.deltaTime;
            if (timer >= 3.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
                timer = 0.0f;
            }
        }
    }
}
