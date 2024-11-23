using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsInstruction : MonoBehaviour
{
    bool left_mouse_button_pressed = false;
    bool left_shift_pressed = false;
    bool left_control_pressed = false;
    bool spacebar_pressed = false;

    void Update()
    {
        if (Input.GetButtonDown("Attack") && !left_mouse_button_pressed)
        {
            left_mouse_button_pressed = true;

            checkIfAllPressed();
        }

        if (Input.GetButtonDown("Sprint") && !left_shift_pressed)
        {
            left_shift_pressed = true;

            checkIfAllPressed();
        }

        if (Input.GetButtonDown("Sneak") && !left_control_pressed)
        {
            left_control_pressed = true;

            checkIfAllPressed();
        }

        if (Input.GetButtonDown("Jump") && !spacebar_pressed)
        {
            spacebar_pressed = true;

            checkIfAllPressed();
        }
    }

    void checkIfAllPressed()
    {
        if (left_mouse_button_pressed &&
            left_shift_pressed &&
            left_control_pressed &&
            spacebar_pressed)
        {
            Destroy(gameObject);
        }
    }
}
