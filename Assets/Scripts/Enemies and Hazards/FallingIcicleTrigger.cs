using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIcicleTrigger : MonoBehaviour
{
    [SerializeField] FallingIcicle falling_icicle;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            falling_icicle.startFall();
        }
    }
}
