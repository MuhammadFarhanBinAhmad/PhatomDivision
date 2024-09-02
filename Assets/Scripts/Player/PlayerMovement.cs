using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int p_MovementSpeed = 5;
    public int p_DashSpeed = 20; // Speed multiplier during a dash
    public float p_DashDuration = 0.2f; // How long the dash lasts
    public float p_DashCooldown = 1.0f; // Cooldown time between dashes

    public bool isDashing = false;
    private float dashTime;
    private float dashCooldownTime;

    public GameObject trail;

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        // Check for dash input and handle dash logic
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && Time.time > dashCooldownTime)
        {
            StartDash();
        }

        // Handle regular movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(-moveHorizontal, 0.0f, -moveVertical) * p_MovementSpeed * Time.deltaTime;

        if (isDashing)
        {
            movement *= p_DashSpeed / p_MovementSpeed; // Multiply movement speed by dash speed multiplier
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                EndDash();
            }
        }

        transform.Translate(movement);
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = p_DashDuration;
        dashCooldownTime = Time.time + p_DashCooldown;
        trail.SetActive(true);
    }

    void EndDash()
    {
        isDashing = false;
        trail.SetActive(false);
    }
}
