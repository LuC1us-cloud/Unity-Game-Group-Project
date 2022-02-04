using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MeleeWeapon : Item
{
    public int minDamage;
    public int maxDamage;
    public float attackSpeed;
    public float attackRange;
    public float attackRadius;
    public float attackDelay;
    public float attackDuration;
    public float attackKnockbackDuration;
    public float attackKnockbackDistance;

}

#if UNITY_EDITOR
[CustomEditor(typeof(MeleeWeapon))]
public class MeleeWeaponEditor : ItemEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif