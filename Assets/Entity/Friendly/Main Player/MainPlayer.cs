using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : Entity
{
    public List<Item> inventory = new List<Item>();
    // public bool isInventoryOpen = false;
    // public GameObject inventoryUI;
    private Item Head;
    private Item Chest;
    private Item Legs;
    private Item Feet;
    public GameObject leftHand;
    public GameObject rightHand;
    private Item Ring;
    private Item Necklace;
    private Item Amulet;
    private Item Belt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if(rightHand != null){
                rightHand.GetComponent<RangedWeapon>().Shoot();
            }
        }
        if (Input.GetButton("Fire2"))
        {
            if(leftHand != null){
                leftHand.GetComponent<RangedWeapon>().Shoot();
            }
        }
        
    }
}
