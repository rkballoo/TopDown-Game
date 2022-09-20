using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig;
    public Animator anim;

    public float moveSpeed;
    public float rotateSpeed;

    public float maxHP;
    public float currHP;

    private bool isMoving;
    private float horizontal;
    private float vertical;
    private Vector2 movementDirection;
    private float movementMagnitude;

    // Update is called once per frame
    void Update()
    {
        // Get inputs for direction of movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Obatin magnitude clamped between 0 and 1 from the vector and normalize direction
        movementDirection = new Vector2(horizontal, vertical);
        movementMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();

        // Set isMoving bool
        isMoving = movementDirection != Vector2.zero;
        anim.SetBool("isMoving", isMoving);
    }

    // Movement and rotation of player were initially in Update() but movement was jumping (seemingly every time
    // the player object rotated), so it was moved to FixedUpdate(), which fixed the issue.
    private void FixedUpdate()
    {
        rig.velocity = movementDirection * movementMagnitude * moveSpeed;

        // If moving, move and rotate player in direction of movement
        if (isMoving)
        {
            // Used Rigidbody velocity instead for smoother physics collisions
            //transform.Translate(movementDirection * movementMagnitude * moveSpeed * Time.deltaTime, Space.World);
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed);
        }
    }
}
