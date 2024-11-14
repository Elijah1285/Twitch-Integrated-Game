using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] Vector3 rotation;

    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
