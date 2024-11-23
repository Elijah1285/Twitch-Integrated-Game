using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    bool activated = false;

    [SerializeField] Material activated_material;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activated)
        {
            other.GetComponent<PlayerHealth>().setCheckPoint(transform.position);  
            GetComponent<Renderer>().material = activated_material;
            activated = true;
        }
    }
}
