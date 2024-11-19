using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryShootingEnemy : MonoBehaviour
{
    float fire_timer;

    Enemy enemy;

    [SerializeField] bool set_direction_projectiles;
    [SerializeField] float time_to_fire;
    [SerializeField] float firing_offset;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firing_point;

    void Start()
    {
        fire_timer = time_to_fire + firing_offset;
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (enemy.active)
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
        GameObject current_projectile = Instantiate(projectile, firing_point.position, transform.rotation);

        if (set_direction_projectiles)
        {
            current_projectile.GetComponent<SetDirectionProjectile>().setMoveVector(transform.forward);
        }
    }
}
