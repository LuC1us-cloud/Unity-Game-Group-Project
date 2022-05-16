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
    private float currAttackDuration; //the duration of the current attack
    private bool canAttack;
    public Animator animator;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Attack();
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                other.gameObject.GetComponent<Entity>().TakeDamage(Random.Range(minDamage, maxDamage));
                Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
                break;
            default:
                break;
        }
    }
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