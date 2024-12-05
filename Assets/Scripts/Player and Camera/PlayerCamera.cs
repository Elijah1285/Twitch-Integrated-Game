using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float x_sensitivity;
    [SerializeField] float y_sensitivity;

    [SerializeField] Transform orientation;
    [SerializeField] Transform camera_position;

    float x_rotation;
    float y_rotation;

    private void Update()
    {
        //mouse input
        float mouse_x = Input.GetAxisRaw("Mouse X") * x_sensitivity * Time.deltaTime;
        float mouse_y = Input.GetAxisRaw("Mouse Y") * y_sensitivity * Time.deltaTime;

        x_rotation -= mouse_y;
        y_rotation += mouse_x;

        x_rotation = Mathf.Clamp(x_rotation, -90.0f, 90.0f);

        //rotate camera and player orientation
        transform.rotation = Quaternion.Euler(x_rotation, y_rotation, 0.0f);
        orientation.rotation = Quaternion.Euler(0.0f, y_rotation, 0.0f);

        //set camera position
        transform.position = camera_position.position;
    }
}
