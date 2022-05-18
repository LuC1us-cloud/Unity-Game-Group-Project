using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Armor,
        MeleeWeapon,
        RangedWeapon
    }
    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    private GameObject self;
    [HideInInspector]
    public string itemName;
    [HideInInspector]
    public string description;
    [HideInInspector]
    public int value;
    public Sprite sprite;
    [HideInInspector]
    public bool stackable;
    [HideInInspector]
    public int maxStack;
    [HideInInspector]
    public int quantity;
    [HideInInspector]
    public bool consumable;
    [HideInInspector]
    public bool equippable;
    [HideInInspector]
    public bool isEquipped;
    [HideInInspector]
    public ItemType type;
    [HideInInspector]
    public ItemRarity rarity;
    public bool isOnGround = true;
    private void Start()
    {
        self = gameObject;
    }

    /// <summary>
    /// Use this function to drop the item, returns amount of items left in the stack
    /// </summary>
    public int Drop(Transform position, int angle = -1)
    {
        if (angle < 0)
        {
            angle = Random.Range(0, 360);
        }
        // if the item is stackable and the quantity is greater than 1
        // then decrement the quantity and return
        // if (stackable && quantity > 1)
        // {
        //     quantity--;
        //     Instantiate(self, position.position, position.rotation);
        //     return quantity;
        // }
        // activate item
        self.SetActive(true);
        isOnGround = true;
        isEquipped = false;
        transform.position = new Vector3(position.position.x, position.position.y, transform.position.z);
        // change the rotation to random rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
        return 0;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Item item = (Item)target;
        item.itemName = EditorGUILayout.TextField("Item Name", item.itemName);
        item.description = EditorGUILayout.TextField("Description", item.description);
        item.value = EditorGUILayout.IntField("Value", item.value);
        item.stackable = EditorGUILayout.Toggle("Stackable", item.stackable);
        if (item.stackable)
        {
            item.maxStack = EditorGUILayout.IntField("Max Stack", item.maxStack);
        }
        item.consumable = EditorGUILayout.Toggle("Consumable", item.consumable);
        item.equippable = EditorGUILayout.Toggle("Equippable", item.equippable);
        if (item.equippable)
        {
            item.isEquipped = EditorGUILayout.Toggle("Is Equipped", item.isEquipped);
        }
        item.type = (Item.ItemType)EditorGUILayout.EnumPopup("Type", item.type);
        item.rarity = (Item.ItemRarity)EditorGUILayout.EnumPopup("Rarity", item.rarity);
    }
}
#endif