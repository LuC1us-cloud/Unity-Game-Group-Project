using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : Entity
{
    public List<Item> inventory = new List<Item>();
    public GameObject inventoryUI;
    public GameObject leftHandSlot;
    public GameObject rightHandSlot;
    private GameObject leftArm;
    private GameObject rightArm;
    private bool lastEquipedLeft;
    private void Start()
    {
        leftArm = GameObject.Find("Left Arm");
        rightArm = GameObject.Find("Right Arm");
    }
    void Update()
    {
        // if tab is pressed toggle inventory GameObject visibility
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            // get child of inventoryUI GameObject nad call Inventory.UpdateContent()
            inventoryUI.transform.GetChild(0).GetComponent<InventoryController>().UpdateContent();
            // and pause the game
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
        if (Time.timeScale == 0) return;

        // if e is pressed call function called use()
        if (Input.GetKeyDown(KeyCode.E))
        {
            Use();
        }
        if (Input.GetButton("Fire1"))
        {
            if (leftHandSlot == null) return;
            leftHandSlot.GetComponent<RangedWeapon>().Shoot();
        }
        if (Input.GetButton("Fire2"))
        {
            if (rightHandSlot == null) return;
            rightHandSlot.GetComponent<RangedWeapon>().Shoot();
        }
    }
    void Use()
    {
        // if the GameObject directly under the cursor has an Interactable component
        // and that GameObject is in radius of 1.5f of the MainPlayer, call the Interact() function
        // else search for the nearest Item and call the PickUp() function

        // get the GameObject under the cursor, by raycasting from cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        if (hit.collider != null)
        {
            // check if the GameObject under the cursor is closer than 2 to the MainPlayer
            if (Vector2.Distance(transform.position, hit.collider.transform.position) < 2f)
            {
                // if the GameObject under the cursor has an Interactable component
                if (hit.collider.GetComponent<Interactable>() != null)
                {
                    // call the Interact() function
                    hit.collider.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
        // get all gameObjects with tag "Item" in radius of 1.5f of the MainPlayer
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        // loop through all items
        GameObject closest = null;
        foreach (Collider2D item in items)
        {
            // if the item is an Item
            if (item.gameObject.tag == "Item")
            {
                // if the closest item is null or the item is closer than the closest item
                if (closest == null ||
                Vector2.Distance(transform.position, item.transform.position) <
                Vector2.Distance(transform.position, closest.transform.position))
                {
                    // set the closest item to the item
                    closest = item.gameObject;
                }
            }
        }
        // if the closest item is not null
        if (closest != null)
        {
            // call the PickUp() function
            PickUp(closest);
        }

    }
    void PickUp(GameObject item)
    {
        // if the inventory is full, return
        if (inventory.Count >= 100) return;
        // if the item is an Item
        if (item.tag == "Item" && item.GetComponent<Item>().isOnGround)
        {
            // add the Item to the inventory
            inventory.Add(item.GetComponent<Item>());
            // set item.isOnGround to false
            item.GetComponent<Item>().isOnGround = false;
            // disable the item
            item.SetActive(false);
        }
    }
    public void EquipItem(GameObject item)
    {
        if (item.GetComponent<Item>().isEquipped) return;
        if (leftHandSlot == null)
        {
            leftHandSlot = item;
            item.transform.SetParent(leftArm.transform);
        }
        else if (rightHandSlot == null)
        {
            rightHandSlot = item;
            item.transform.SetParent(rightArm.transform);
        }
        else
        {
            if (lastEquipedLeft)
            {
                leftHandSlot.GetComponent<Item>().Drop(transform);
                leftHandSlot.transform.SetParent(null);
                inventory.Remove(leftHandSlot.GetComponent<Item>());
                leftHandSlot = item;
                item.transform.SetParent(leftArm.transform);
                lastEquipedLeft = false;
            }
            else
            {
                rightHandSlot.GetComponent<Item>().Drop(transform);
                rightHandSlot.transform.SetParent(null);
                inventory.Remove(rightHandSlot.GetComponent<Item>());
                rightHandSlot = item;
                item.transform.SetParent(rightArm.transform);
                lastEquipedLeft = true;
            }
        }
        // make sure the items scale stays the same
        item.GetComponent<Item>().isEquipped = true;
        item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.localPosition = new Vector3(0, 0, 0);
        item.transform.localRotation = Quaternion.identity;
        item.SetActive(true);
    }

    //Method to move blocks with the tag 'Movable'
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Movable"))
        {
            if (hit.collider.gameObject.GetComponent<Rigidbody>() == null) return;
            var pushDir = new Vector3(hit.moveDirection.x, 0, 0);
            hit.collider.attachedRigidbody.velocity = pushDir * 2.5f;   //multiply by push strength
        }
    }
}
