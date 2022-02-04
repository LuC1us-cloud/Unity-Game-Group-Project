using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RangedWeapon : Item
{
    public int minDamage;
    public int maxDamage;
    [Header("Attacks per second")]
    public float attackSpeed;
    [Header("Seconds that the bullet will be alive")]
    public float attackRange;
    [Header("1 is fully accurate, 0.01 is fully inaccurate")]
    public float accuracy;
    public float attackDelay;
    public float attackKnockbackDuration;
    public float attackKnockbackDistance;
}

#if UNITY_EDITOR
[CustomEditor(typeof(RangedWeapon))]
public class RangedWeaponEditor : ItemEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif