using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    private GameObject mainPlayer;
    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = GameObject.Find("Main Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interact()
    {
        if (mainPlayer.GetComponent<PlayerMovement>().box != null)
        {
            mainPlayer.GetComponent<PlayerMovement>().box = null;
        }
        else
        {
            mainPlayer.GetComponent<PlayerMovement>().box = gameObject;
        }
    }
}
