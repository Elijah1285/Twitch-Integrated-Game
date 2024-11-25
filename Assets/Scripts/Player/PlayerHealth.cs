using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    int hitpoints;

    [SerializeField] int max_hitpoints;

    [SerializeField] TextMeshProUGUI hitpoints_text;
    [SerializeField] AudioClip hurt_sound;

    //current checkpoint in world space
    Vector3 current_checkpoint;

    void Start()
    {
        hitpoints = max_hitpoints;
        hitpoints_text.text = "HP: " + hitpoints.ToString();

        current_checkpoint = transform.position;
    }

    //passing in a positive integer will damage, a negative one will heal
    public void takeDamageOrHeal(int damage)
    {
        hitpoints -= damage;
        hitpoints_text.text = "HP: " + hitpoints.ToString();

        if (hitpoints <= 0)
        {
            respawn();
        }

        //check if should play hurt sound
        if (damage > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(hurt_sound);
        }
    }

    public void setCheckPoint(Vector3 new_checkpoint)
    {
        current_checkpoint = new_checkpoint;
    }

    public void respawn()
    {
        GetComponent<CharacterController>().enabled = false;
        this.transform.position = current_checkpoint;
        GetComponent<CharacterController>().enabled = true;
        hitpoints = max_hitpoints;
        hitpoints_text.text = "HP: " + hitpoints.ToString();
        GetComponent<PlayerCoins>().resetCoins();
    }
}
