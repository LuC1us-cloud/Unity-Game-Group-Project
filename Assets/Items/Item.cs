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
    [HideInInspector]
    public string itemName;
    [HideInInspector]
    public string description;
    [HideInInspector]
    public int value;
    [HideInInspector]
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
        item.sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", item.sprite, typeof(Sprite), false);
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