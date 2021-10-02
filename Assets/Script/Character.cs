using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Movements variables")]
    public float maxSpeed;
    float speed;
    public float acceleration;
    public float deceleration;
    public float jumpForce;
    float currJumpForce;
    public float jumpAcceleration;

    public float groundedDelayValue = 2.0f;
    public float groundedDelay;

    public int groundedDelayIncrValue;
    public int groundedDelayIncr;

    public Vector2 perp;
    public Vector2 opposite;
    [Header("Booleans States")]
    public bool isGrounded;
    public bool isChargingJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundedDelay = groundedDelayValue;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!isGrounded)
        {
            groundedDelay -= Time.deltaTime;
        }
        if(groundedDelay <= 0)
        {
            rb.velocity = Physics2D.gravity;
            groundedDelayIncr++;
            groundedDelay = groundedDelayValue;
        }
        if(groundedDelayIncr >= groundedDelayIncrValue)
        {
            transform.position = Physics2D.ClosestPoint(transform.position, GetComponent<CustomGravity>().nearest.GetComponent<BoxCollider2D>());
            groundedDelayIncr = 0;
        }*/
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
            opposite = -GetComponent<CustomGravity>().dirGravity;
            opposite.Normalize();
            if (Input.GetKey("space"))
            {
                isChargingJump = true;
                currJumpForce = Mathf.Clamp(currJumpForce + Time.deltaTime * jumpAcceleration, jumpForce/2, jumpForce);
            }
            if (Input.GetKeyUp("space"))
            {
                isChargingJump = false;
                
                rb.velocity += opposite * currJumpForce;
                Debug.Log(opposite);
                //rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(-Physics2D.gravity.y *currJumpForce));
                currJumpForce = 0;
            }
        }
    }

    private void FixedUpdate()
    {
    }
    private void Move()
    {
        if (isGrounded)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                perp = new Vector2(Physics2D.gravity.y, -Physics2D.gravity.x);

                //FLIP X
                if (gameObject.transform.localScale.x > 0)
                {
                    Vector3 sca = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                    gameObject.transform.localScale = sca;
                }
                speed = Mathf.Abs(Mathf.Clamp((speed + (Input.GetAxisRaw("Horizontal") * Time.deltaTime * acceleration)) , -maxSpeed, maxSpeed));
            }else if(Input.GetAxis("Horizontal") > 0)
            {
                perp = new Vector2(-Physics2D.gravity.y, Physics2D.gravity.x);

                //FLIP X
                if (gameObject.transform.localScale.x < 0)
                {
                    Vector3 sca = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                    gameObject.transform.localScale = sca;
                }
                speed = Mathf.Abs(Mathf.Clamp((speed + (Input.GetAxisRaw("Horizontal") * Time.deltaTime * acceleration)), -maxSpeed, maxSpeed));
                
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
            perp.Normalize();
            if (perp.x != 0)
            {
                rb.velocity = new Vector2(perp.x * speed, rb.velocity.y);
            }
            else if(perp.y != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, perp.y * speed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Ground") || c.gameObject.CompareTag("GravityWall"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionStay2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Ground") || c.gameObject.CompareTag("GravityWall"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Ground") || c.gameObject.CompareTag("GravityWall"))
        {
            isGrounded = false;
        }
    }

    private void OnGUI()
    {
        GUILayout.Label(" Velocity = " + rb.velocity);
        GUILayout.Label(" perp = " + perp);
        GUILayout.Label(" Speed = " + speed);
    }
}
