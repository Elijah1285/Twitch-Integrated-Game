using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float lifetime;
    [SerializeField] float speed;
    [SerializeField] int damage;

    void Update()
    {
        //calculate movement vector
        Vector3 vector = Vector3.forward * speed * Time.deltaTime;

        //move the projectile
        transform.Translate(vector);

        //update lifetime
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
}
