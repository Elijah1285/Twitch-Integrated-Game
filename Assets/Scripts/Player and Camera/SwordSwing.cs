using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    

    Quaternion initial_rotation;
    Quaternion target_rotation;

    [SerializeField] float swing_angle;
    [SerializeField] float swing_speed;

    void Start()
    {
        initial_rotation = transform.localRotation;
    }

    void Update()
    {
        
    }

    public void swing()
    {
        target_rotation = initial_rotation * Quaternion.Euler(0.0f, 0.0f, swing_angle);
        StartCoroutine(SwingingSword());
    }

    IEnumerator SwingingSword()
    {
        float elapsed = 0.0f;

        while (elapsed < 1.0f)
        {
            elapsed += Time.deltaTime * swing_speed;
            transform.localRotation = Quaternion.Slerp(initial_rotation, target_rotation, elapsed);
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < 1.0f)
        {
            elapsed += Time.deltaTime * swing_speed;
            transform.localRotation = Quaternion.Slerp(target_rotation, initial_rotation, elapsed);
            yield return null;
        }

        transform.localRotation = initial_rotation;
    }
}
