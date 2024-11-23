using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCoins : MonoBehaviour
{
    int coin_count = 0;
    [SerializeField] TextMeshProUGUI coins_text;

    public void collectCoin()
    {
        coin_count++;

        if (coin_count >= 10)
        {
            GetComponent<PlayerHealth>().takeDamageOrHeal(-5);
        }

        coins_text.text = "Coins: " + coin_count.ToString();
        Destroy(gameObject);
    }
}
