using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidBody2D;
    [SerializeField] private float m_speed;

    [SerializeField] private bool m_respectsGravity;

    private void FixedUpdate()
    {
        Movement();
    }
    void Movement ()
    {
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
        } else
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

    private void Start()
    {
        //the character doesn't fall in some of the levels
        if (m_respectsGravity)
            m_rigidBody2D.bodyType = RigidbodyType2D.Kinematic;
    }
}
