using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCoins : MonoBehaviour
{
    int coin_count = 0;

    [SerializeField] TextMeshProUGUI coins_text;
    [SerializeField] AudioClip coin_collect_sound;
    [SerializeField] AudioSource one_shot_audio_source;

    void Start()
    {
        coins_text.text = "Coins: " + coin_count.ToString();
    }

    public void collectCoin()
    {
        coin_count++;

        if (coin_count >= 20)
        {
            GetComponent<PlayerHealth>().takeDamageOrHeal(-5);
            coin_count = 0;
        }

        coins_text.text = "Coins: " + coin_count.ToString();

        one_shot_audio_source.PlayOneShot(coin_collect_sound);
    }

    public void resetCoins()
    {
        coin_count = 0;
        coins_text.text = "Coins: " + coin_count.ToString();
    }
}
