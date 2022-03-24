using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum SpecialAbility
    {
        None,
        Dash,
        Forcefield_Pushaway,
        Forcefield_Shield,
        Forcefield_Damage,
        Gravity_Orb,
        Grappling_Hook,
    }
    public GameObject Ff_PushawayPrefab;
    public GameObject Ff_ShieldPrefab;
    public GameObject Ff_DamagePrefab;
    public GameObject GravityOrbPrefab;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    [Header("Double Shift Usage")]
    public SpecialAbility specialAbility = SpecialAbility.None;
    public float dashDistance = 20f;

    Vector2 mousePos;
    Vector2 movement;
    bool shiftClickedOnce = false;
    bool shiftClickedTwice = false;
    float shiftClickInterval = 0.5f;
    // float SpecialAbilityActiveTime = 0;
    // float SpecialAbilityDuration = 4f;

    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    private void Update() {
        // Temporary for testing purposes
        // if 'r' is clicked, cycle through special abilities
        if (Input.GetKeyDown(KeyCode.R)) {
            specialAbility = (SpecialAbility)(((int)specialAbility + 1) % System.Enum.GetValues(typeof(SpecialAbility)).Length);
        }

        //-------------------------------

        if (shiftClickedOnce && Input.GetKeyDown(KeyCode.LeftShift) && !shiftClickedTwice && 0 < shiftClickInterval)
        {
            shiftClickedTwice = true;
            shiftClickInterval = 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftClickedOnce = true;
        }
        if (shiftClickedOnce && !shiftClickedTwice)
        {
            shiftClickInterval -= Time.deltaTime;
            if (shiftClickInterval <= 0)
            {
                shiftClickedOnce = false;
                shiftClickedTwice = false;
                shiftClickInterval = 0.5f;
            }
        }

        if (shiftClickedTwice)
        {
            switch (specialAbility)
            {
                case SpecialAbility.Dash:
                    Dash();
                    break;

                case SpecialAbility.Forcefield_Pushaway:
                    Forcefield_Pushaway();
                    break;

                case SpecialAbility.Forcefield_Shield:
                    Forcefield_Shield();
                    break;

                case SpecialAbility.Forcefield_Damage:
                    Forcefield_Damage();
                    break;

                case SpecialAbility.Gravity_Orb:
                    Gravity_Orb();
                    break;
                default:
                    break;
            }
            shiftClickedOnce = false;
            shiftClickedTwice = false;
        }
    }
    private void Dash()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime * dashDistance);
    }
    private void Forcefield_Pushaway()
    {
        Instantiate(Ff_PushawayPrefab, rb.position, Quaternion.identity);
    }
    private void Forcefield_Shield()
    {
        Instantiate(Ff_ShieldPrefab, rb.position, Quaternion.identity);
    }
    private void Forcefield_Damage()
    {
        Instantiate(Ff_DamagePrefab, rb.position, Quaternion.identity);
    }
    private void Gravity_Orb()
    {
        var gravOrb = Instantiate(GravityOrbPrefab, rb.position, Quaternion.identity);
        // change velocity of the gravOrb to move away from the player, torwards the cursor
        gravOrb.GetComponent<Rigidbody2D>().velocity = (mousePos - rb.position).normalized * 3f;
    }
}
