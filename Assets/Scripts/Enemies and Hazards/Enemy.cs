using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool active = false;

    public void activate()
    {
        active = true;
    }
}
