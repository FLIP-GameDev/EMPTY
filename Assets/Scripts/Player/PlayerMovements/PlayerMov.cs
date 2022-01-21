using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public CharacterController controller;

    float StartingSpeed;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float SprintcameraFOVTime = 0.001f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;

    public float normalFOV;

    Vector3 velocity;
    bool isGrounded;
    void Start()
    {
        StartingSpeed = speed;
        normalFOV = Camera.main.fieldOfView;
    }
    void Update()
    {
        //Movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Sprinting
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, normalFOV + 30, SprintcameraFOVTime);
            speed = StartingSpeed * 2;
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, normalFOV, SprintcameraFOVTime);
            speed = StartingSpeed;
        }
    }
}