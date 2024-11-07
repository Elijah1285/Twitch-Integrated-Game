using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryShootingEnemy : MonoBehaviour
{
    bool active = false;
    float fire_timer;

    [SerializeField] bool active_at_start;
    [SerializeField] float time_to_fire;
    [SerializeField] float firing_offset;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firing_point;

    void Start()
    {
        fire_timer = time_to_fire + firing_offset;
        active = active_at_start;
    }

    void Update()
    {
        if (active)
        {
            fire_timer -= Time.deltaTime;

            if (fire_timer <= 0)
            {
                fire();

                fire_timer = time_to_fire;
            }
        }
    }

    void fire()
    {
        Instantiate(projectile, firing_point.position, transform.rotation);
    }
}
