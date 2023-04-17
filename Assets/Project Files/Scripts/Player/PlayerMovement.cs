using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidBody2D;
    [SerializeField] private float m_speed;

    [SerializeField] private bool m_respectsGravity;
    [SerializeField] private bool m_rotate;

    //Dash
    [SerializeField] private bool m_canDash;
    [SerializeField] private bool m_isDashing;
    [SerializeField] private float m_dashForce;
    [SerializeField] private float m_dashTime;
    [SerializeField] private float m_dashCoolDown;

    [SerializeField] private bool m_dashEnabled;

    [SerializeField] private SkeletonAnimation m_skeletonAnimation;
    private void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        if (m_isDashing) { return; }
        //restrict player movement to the bounds of the screen
        //Get the screen bounds to determine how far the player can move
        Vector2 screenBounds = new Vector2(Screen.width, Screen.height);
        Vector2 worldBounds = Camera.main.ScreenToWorldPoint(screenBounds);

        //Character movement based on controller movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //the bounds 
        float upperBound = worldBounds.y;
        float leftBound = -worldBounds.x;


        if (transform.position.y < upperBound && transform.position.x > leftBound)
        {
            m_rigidBody2D.velocity = new Vector2(horizontalInput, verticalInput) * m_speed;
        }
        else
        {
            //Set left and upwards velocity to zero if player tries to move out of the bounds
            if (transform.position.x < leftBound)
            {
                if (horizontalInput < 0.1f)
                {
                    horizontalInput = 0.0f;
                }
            }
            else if (transform.position.y > upperBound)
            {
                if (verticalInput > 0.1f)
                {
                    verticalInput = 0.0f;
                }
            }
            m_rigidBody2D.velocity = new Vector2(horizontalInput, verticalInput) * m_speed;
        }
    }

    private void Update()
    {
        if (m_rotate)
        {
            RotatePlayer();
        }

        if (Input.GetButtonDown("Fire2") && m_canDash && m_dashEnabled)
        {
            StartCoroutine(Dash());
        }

    }

    private void Start()
    {
        //the character doesn't fall in some of the levels
        if (!m_respectsGravity)
            m_rigidBody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    private void RotatePlayer()
    {
        if (m_isDashing)
        {
            return;
        }
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(horizontalInput, verticalInput);
        moveDir.Normalize();

        if (moveDir != Vector2.zero)
        {
            Quaternion playerRotation = Quaternion.LookRotation(Vector3.forward, moveDir);
            Quaternion playerRotationOffset = Quaternion.Euler(0f, 0f, 90f) * playerRotation;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotationOffset, 720f * Time.deltaTime);
        }
    }

    private IEnumerator Dash()
    {
        m_canDash = false;
        m_isDashing = true;
        m_rigidBody2D.velocity = new Vector2(transform.localScale.x * m_dashForce * m_skeletonAnimation.skeleton.ScaleX, 0f);
        yield return new WaitForSeconds(m_dashTime);
        m_isDashing = false;
        yield return new WaitForSeconds(m_dashCoolDown);
        m_canDash = true;
    }
}
