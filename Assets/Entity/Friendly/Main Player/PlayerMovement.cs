using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum DoubleShiftUsage
    {
        None,
        Dash,
    }
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    [Header("Double Shift Usage")]
    public DoubleShiftUsage specialAbility = DoubleShiftUsage.None;
    public float dashDistance = 20f;

    Vector2 mousePos;
    Vector2 movement;
    bool shiftClickedOnce = false;
    bool shiftClickedTwice = false;
    float shiftClickInterval = 0.5f;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate()
    {
        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (shiftClickedTwice)
        {
            switch (specialAbility)
            {
                case DoubleShiftUsage.Dash:
                    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime * dashDistance);
                    break;
                
                default:
                    break;
            }
            shiftClickedOnce = false;
            shiftClickedTwice = false;
        }
    }
}
