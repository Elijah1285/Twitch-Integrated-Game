using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VoteController : MonoBehaviour
{
    [SerializeField] float vote_timer;
    [SerializeField] TextMeshProUGUI time_left_text;

    [SerializeField] TextMeshProUGUI ice_cavern_vote_amount_text;
    [SerializeField] TextMeshProUGUI underworld_vote_amount_text;

    int ice_cavern_votes = 0;
    int underworld_votes = 0;

    void Update()
    {
        vote_timer -= Time.deltaTime;

        int int_vote_timer = (int)vote_timer;

        time_left_text.text = int_vote_timer.ToString() + " seconds left";

        if (Input.GetKeyDown("1"))
        {
            vote(VoteOption.ICE_CAVERN);
        }
        else if (Input.GetKeyDown("2"))
        {
            vote(VoteOption.UNDERWORLD);
        }

        if (vote_timer  <= 0)
        {
            endVote();
        }

        

    }

    public void vote(VoteOption vote_option)
    {
        switch(vote_option)
        {
            case VoteOption.ICE_CAVERN:
                {
                    ice_cavern_votes++;

                    ice_cavern_vote_amount_text.text = ice_cavern_votes.ToString();
                    break;
                }

            case VoteOption.UNDERWORLD:
                {
                    underworld_votes++;

                    underworld_vote_amount_text.text = underworld_votes.ToString();
                    break;
                }
        }
    }

    void endVote()
    {
        VoteOption winning_option = VoteOption.ICE_CAVERN;

        if (underworld_votes > ice_cavern_votes)
        {
            winning_option = VoteOption.UNDERWORLD;
        }

        switch(winning_option)
        {
            case VoteOption.ICE_CAVERN:
                {
                    SceneManager.LoadScene("Ice Cavern");

                    break;
                }

            case VoteOption.UNDERWORLD:
                {
                    SceneManager.LoadScene("Underworld");

                    break;
                }
        }
    }


}
