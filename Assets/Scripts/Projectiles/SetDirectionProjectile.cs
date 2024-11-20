using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDirectionProjectile : MonoBehaviour
{
    Vector3 move_vector;

    void Update()
    {
        transform.Translate(move_vector * GetComponent<Projectile>().getSpeed() * Time.deltaTime, Space.World);
    }

    public void setMoveVector(Vector3 new_move_vector)
    {
        move_vector = new_move_vector;
    }
}
