using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [SerializeField] float slow_down_multiplier;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().setSlowDownMultiplier(slow_down_multiplier);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().setSlowDownMultiplier(1.0f);
        }
    }
}
