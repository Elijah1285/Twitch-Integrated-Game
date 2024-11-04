using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float attack_timer = 0.0f;
    List<GameObject> enemies_in_attack_box = new List<GameObject>();

    [SerializeField] float time_for_attack;
    [SerializeField] float attack_range;
    [SerializeField] int attack_damage;

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
        if (other.tag == "Enemy" && !enemies_in_attack_box.Contains(other.gameObject))
        {
            enemies_in_attack_box.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && enemies_in_attack_box.Contains(other.gameObject))
        {
            enemies_in_attack_box.Remove(other.gameObject);
        }
    }

    void attack()
    {
        for (int i = 0; i < enemies_in_attack_box.Count; i++)
        {
            enemies_in_attack_box[i].GetComponent<EnemyHealth>().takeDamage(attack_damage);
        }
    }

    public void removeEnemy(GameObject enemy)
    {
        enemies_in_attack_box.Remove(enemy);
    }
}
