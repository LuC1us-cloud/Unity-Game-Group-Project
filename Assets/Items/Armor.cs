using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Armor : Item
{
    public int armor;

}

#if UNITY_EDITOR
[CustomEditor(typeof(Armor))]
public class ArmorEditor : ItemEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif