using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float bounce_force;
    [SerializeField] bool cause_acceleration;

    void OnTriggerEnter(Collider other)
    {
        //make sure it is the player and they are falling
        if (other.tag == "Player" && other.GetComponent<Rigidbody>().velocity.y < 0.0f)
        {
            other.GetComponent<PlayerMovement>().padJump(bounce_force, cause_acceleration);
        }
    }
}
