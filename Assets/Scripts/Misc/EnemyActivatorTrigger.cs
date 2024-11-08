using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivatorTrigger : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies_to_activate = new List<Enemy>();

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (Enemy enemy in enemies_to_activate)
            {
                enemy.activate();
            }
        }
    }
}
