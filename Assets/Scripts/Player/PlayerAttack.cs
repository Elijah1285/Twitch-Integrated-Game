using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float attack_timer = 0.0f;

    [SerializeField] float time_for_attack;
    [SerializeField] int attack_damage;

    EnemyHealth target_enemy;

    void Start()
    {
        attack_timer = time_for_attack;
    }

    void Update()
    {
        if (attack_timer > 0.0f)
        {
            attack_timer -= Time.deltaTime;
        }

        if (Input.GetButton("Attack"))
        {
            if (attack_timer <= 0.0f)
            {
                attack();

                attack_timer = time_for_attack;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Enemy" && !enemies_in_attack_box.Contains(other.gameObject))
        {
            enemies_in_attack_box.Add(other.gameObject);
            Debug.Log("added");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && enemies_in_attack_box.Contains(other.gameObject))
        {
            enemies_in_attack_box.Remove(other.gameObject);
            Debug.Log("removed");
        }
    }

    void attack()
    {
        foreach(GameObject enemy in enemies_in_attack_box)
        {
            enemy.GetComponent<EnemyHealth>().takeDamage(attack_damage);
            Debug.Log("a");
        }
    }
}
