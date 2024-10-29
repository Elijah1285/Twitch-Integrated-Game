using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryShootingEnemy : MonoBehaviour
{
    float fire_timer;
    [SerializeField] float time_to_fire;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firing_point;

    void Start()
    {
        fire_timer = time_to_fire;
    }

    void Update()
    {
        fire_timer -= Time.deltaTime;

        if (fire_timer <= 0)
        {
            fire();

            fire_timer = time_to_fire;
        }
    }

    void fire()
    {
        Instantiate(projectile, firing_point.position, transform.rotation);
    }
}
