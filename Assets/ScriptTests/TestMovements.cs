using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovements : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;


    public GameObject gravityCenter;
    Vector3 velocity, desiredVelocity;
    Vector2 playerInput;
    Rigidbody2D body;


    //JUMP
    bool desiredJump;
    [SerializeField, Range(0f, 10f)]
    float jumpHeight = 2f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {

        playerInput.x = 0;
        playerInput.y = 0;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        Vector3 acceleration = new Vector3(playerInput.x, playerInput.y, 0) * maxAcceleration;
        desiredVelocity = new Vector3(playerInput.x, playerInput.y, 0) * maxSpeed;


        desiredJump |= Input.GetButtonDown("Jump");


    }
    private void FixedUpdate()
    {

        velocity = body.velocity;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);
        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }
        body.velocity = velocity;

    }
    void Jump()
    {
        velocity.y += Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
    }

}
