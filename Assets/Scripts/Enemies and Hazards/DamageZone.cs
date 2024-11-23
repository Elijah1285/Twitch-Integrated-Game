using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    float damage_timer;
    [SerializeField] float time_to_damage;

    [SerializeField] int damage;

    void Start()
    {
        damage_timer = time_to_damage;
    }

    void Update()
    {
        if (damage_timer > 0.0f)
        {
            damage_timer -= Time.deltaTime;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (damage_timer <= 0)
            {
                other.GetComponent<PlayerHealth>().takeDamageOrHeal(damage);

                damage_timer = time_to_damage;
            }
        }
    }
}
