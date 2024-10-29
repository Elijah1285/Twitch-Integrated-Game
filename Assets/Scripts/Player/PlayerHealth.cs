using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int hitpoints;
    Canvas canvas;
    [SerializeField] TextMeshProUGUI hitpoints_text;

    void Start()
    {
        hitpoints_text.text = "HP: " + hitpoints.ToString();
    }

    public void takeDamage(int damage)
    {
        hitpoints -= damage;
        hitpoints_text.text = "HP: " + hitpoints.ToString();

        if (hitpoints <= 0)
        {
            killPlayer();
        }
    }

    void killPlayer()
    {
        SceneManager.LoadScene("Game Over");
    }
}
