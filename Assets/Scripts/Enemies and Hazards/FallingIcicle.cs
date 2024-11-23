using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIcicle : MonoBehaviour
{
    float lifetime = 5.0f;

    bool falling = false;
    float fall_velocity = 0.0f;

    [SerializeField]float fall_acceleration;
    [SerializeField] float terminal_velocity;

    [SerializeField] int damage;

    void Update()
    {
        if (falling)
        {
            //fall
            if (fall_velocity < terminal_velocity)
            {
                fall_velocity -= fall_acceleration * Time.deltaTime;

                if (fall_velocity > terminal_velocity)
                {
                    fall_velocity = terminal_velocity;
                }
            }

            Vector3 movement_vector = new Vector3(0.0f, fall_velocity, 0.0f) * Time.deltaTime;

            transform.Translate(movement_vector);

            //update and check lifetime
            lifetime -= Time.deltaTime;

            if (lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().takeDamageOrHeal(damage);

            Destroy(gameObject);
        }
        else if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

    public void startFall()
    {
        falling = true;
    }
}
