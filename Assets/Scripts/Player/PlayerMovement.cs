using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movement_speed;
    [SerializeField] float rotation_speed;
    [SerializeField] float jump_height;
    [SerializeField] float gravity;

    Vector3 velocity;

    CharacterController character_controller;

    void Start()
    {
        character_controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        movePlayer();
        rotatePlayer();
    }

    void movePlayer()
    {
        //get movement inputs
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");

        //apply movement inputs
        //calculate movement magnitude
        velocity.x = horizontal_input * movement_speed * Time.deltaTime;
        velocity.z = vertical_input * movement_speed * Time.deltaTime;

        //check jump input
        if (Input.GetButtonDown("Jump") && character_controller.isGrounded)
        {
            //work out y velocity based on target jump height
            velocity.y = Mathf.Sqrt(jump_height * -2.0f * gravity);
        }

        //apply gravity
        if (!character_controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        //calculate movement direction
        velocity = transform.TransformDirection(velocity);

        //apply calculated movement
        character_controller.Move(velocity * Time.deltaTime);
    }

    void rotatePlayer()
    {
        //get rotation input
        float rotation_input = Input.GetAxis("Mouse X");

        //apply rotation input
        Quaternion target_rotation = transform.rotation * Quaternion.Euler(0, rotation_input * rotation_speed * Time.deltaTime, 0);

        //apply calculated rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, Time.deltaTime * rotation_speed);
    }
}
