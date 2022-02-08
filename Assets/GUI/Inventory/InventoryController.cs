using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject MainPlayerRef;
    public GameObject InventoryRef;
    public GameObject SlotPrefab;
    private void Start() {
        MainPlayerRef = GameObject.Find("Main Player");
    }
    public void UpdateContent() {
        // Get MainPlayer component from InventoryRef GameObject
        // Then foreach item in the inventory list
        // Add a new slotPrefab to gameObject
        // Set the slotPrefab.child(0).child(0).GetComponent<Image>().sprite to the item.sprite

        // Clear all children of InventoryRef GameObject
        foreach (Transform child in InventoryRef.transform) {
            Destroy(child.gameObject);
        }
        MainPlayerRef.GetComponent<MainPlayer>().inventory.ForEach(item =>
        {
            GameObject slot = Instantiate(SlotPrefab, InventoryRef.transform);
            slot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.sprite;
            slot.transform.GetChild(0).gameObject.GetComponent<ButtonController>().item = item;
        });
    }
}
