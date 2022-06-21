using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;
    Vector2 mousePos;
    Vector2 movement;
    public float moveSpeed = 5f;

    public float moveSpeedCoef = 1f;
    
    private void Start()
    {
        moveSpeedCoef = 1f;
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        rb.MovePosition(rb.position + movement * (moveSpeed * moveSpeedCoef) * Time.fixedDeltaTime);
    }
}
