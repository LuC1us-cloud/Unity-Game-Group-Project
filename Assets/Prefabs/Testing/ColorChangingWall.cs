using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangingWall : Interactable
{
    private Timer timer;
    override public void Interact()
    {
        // change to random color
        GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
