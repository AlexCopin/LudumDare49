using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Movements variables")]
    public float maxSpeed;
    public float speed;
    public float acceleration;
    public float deceleration;
    public float jumpForce;
    public float currJumpForce;
    [Header("Booleans States")]
    public bool isGrounded;
    public bool isChargingJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Jump();
        Move();
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKey("space"))
            {
                isChargingJump = true;
                currJumpForce = Mathf.Clamp(currJumpForce + Time.deltaTime * jumpForce, 0, jumpForce);
            }
            if (Input.GetKeyUp("space"))
            {
                isChargingJump = false;
                rb.velocity = new Vector2(rb.velocity.x, currJumpForce);
                currJumpForce = 0;
            }
        }
    }
    private void Move()
    {
        if (isGrounded)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                speed = Mathf.Clamp((speed + Input.GetAxisRaw("Horizontal") * Time.deltaTime * acceleration) , -maxSpeed, maxSpeed);
            }
            else
            {
                if(speed > 0)
                {
                    speed = Mathf.Clamp(speed - Time.deltaTime * deceleration, 0, speed);
                }
                else if(speed < 0)
                {
                    speed = Mathf.Clamp(speed + Time.deltaTime * deceleration, speed, 0);
                }
            }
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
