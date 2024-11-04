using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitpoints;

    public void takeDamage(int damage)
    {
        hitpoints -= damage;

        if (hitpoints < 0)
        {
            PlayerAttack player_attack = FindObjectOfType<PlayerAttack>();
            player_attack.removeEnemy(this.gameObject);

            Destroy(gameObject);
        }
    }
}
