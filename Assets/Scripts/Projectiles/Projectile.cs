using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] bool use_this_movement;

    [SerializeField] float lifetime;
    [SerializeField] float speed;
    [SerializeField] int damage;

    void Update()
    {
        Vector3 vector = new Vector3(0.0f, 0.0f, 0.0f);

        //calculate movement vector
        if (use_this_movement)
        {
            vector = Vector3.forward * speed * Time.deltaTime;
        }

        //move the projectile
        transform.Translate(vector);

        //update and check lifetime
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().takeDamage(damage);
        }
    }

    public float getSpeed()
    {
        return speed;
    }
}
