using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float camera_pitch = 0.0f;

    [SerializeField] float movement_speed;
    [SerializeField] float rotation_speed;
    [SerializeField] float jump_height;
    [SerializeField] float gravity;
    [SerializeField] Transform camera_transform;

    Vector3 velocity;

    CharacterController character_controller;

    void Start()
    {
        character_controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        movePlayer();
        rotatePlayerAndCamera();
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

    void rotatePlayerAndCamera()
    {
        //get rotation input
        float x_rotation_input = Input.GetAxis("Mouse X");
        float y_rotation_input = Input.GetAxis("Mouse Y");

        //apply rotation input
        Quaternion target_player_rotation = transform.rotation * Quaternion.Euler(0.0f, x_rotation_input * rotation_speed * Time.deltaTime, 0.0f);

        float target_camera_pitch = camera_pitch + (-y_rotation_input * rotation_speed * Time.deltaTime);
        camera_pitch = Mathf.Lerp(camera_pitch, target_camera_pitch, Time.deltaTime * rotation_speed);

        if (camera_pitch > 90.0f)
        {
            camera_pitch = 90.0f;
        }
        else if (camera_pitch < -90.0f)
        {
            camera_pitch = -90.0f;
        }

        //apply calculated rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target_player_rotation, Time.deltaTime * rotation_speed);
        
        camera_transform.localRotation = Quaternion.Euler(camera_pitch, 0.0f, 0.0f);
    }
}
