using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIcicle : MonoBehaviour
{
    bool falling = false;
    float fall_velocity = 0.0f;

    [SerializeField]float fall_acceleration;
    [SerializeField] float terminal_velocity;

    [SerializeField] int damage;

    void Update()
    {
        if (falling)
        {
            if (fall_velocity < terminal_velocity)
            {
                fall_velocity -= fall_acceleration * Time.deltaTime;

                if (fall_velocity > terminal_velocity)
                {
                    fall_velocity = terminal_velocity;
                }
            }

            transform.Translate(new Vector3(0.0f, fall_velocity, 0.0f));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().takeDamage(damage);

            Destroy(gameObject);
        }
    }

    public void startFall()
    {
        falling = true;
    }
}
