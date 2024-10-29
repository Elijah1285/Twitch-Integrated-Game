using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float movement_speed = 5.0f;
    float rotation_speed = 1000.0f;

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
        // get movement inputs
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");

        // apply movement inputs
        // calculate movement magnitude
        Vector3 movement = new Vector3(horizontal_input, 0, vertical_input) * Time.deltaTime * movement_speed;

        // calculate movement direction
        movement = transform.TransformDirection(movement);

        // apply calculated movement
        character_controller.Move(movement);
    }

    void rotatePlayer()
    {
        // get rotation input
        float rotation_input = Input.GetAxis("Mouse X");

        // apply rotation input
        Quaternion target_rotation = transform.rotation * Quaternion.Euler(0, rotation_input * rotation_speed * Time.deltaTime, 0);

        //apply calculated rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, Time.deltaTime * rotation_speed);
    }
}
