using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] Vector3 rotation;

    void Update()
    {
        Quaternion delta_rotation = Quaternion.Euler(rotation * Time.deltaTime);

        transform.rotation *= delta_rotation;
    }
}
