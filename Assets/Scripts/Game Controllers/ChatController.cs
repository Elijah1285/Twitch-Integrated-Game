using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatController : MonoBehaviour
{
    public int max_messages = 25;

    public GameObject chat_panel;
    public GameObject text_object;
    public TMP_InputField chat_input_box;

    [SerializeField] ScrollRect chat_scroll_rect;
    [SerializeField] VoteController vote_controller;

    List<string> users_voted = new List<string>();
    [SerializeField] List<Message> message_list = new List<Message>();

    void Update()
    {
        if (chat_input_box.text != "")
        {
            if (Input.GetButtonDown("Submit"))
            {
                sendMessageToChat("<Console>", chat_input_box.text);
                chat_input_box.text = "";
            }
        }
        else
        {
            if (!chat_input_box.isFocused && Input.GetButtonDown("Submit"))
            {
                chat_input_box.ActivateInputField();
            }
        }
    }

    public void sendMessageToChat(string chatter, string message)
    {
        //check if exceeded max messages
        if (message_list.Count >= max_messages)
        {
            Destroy(message_list[0].text_object.gameObject);
            message_list.Remove(message_list[0]);
        }

        //create and send new message
        Message new_message = new Message();
        new_message.text = chatter + ": " + message;
        GameObject new_text = Instantiate(text_object, chat_panel.transform);
        new_message.text_object = new_text.GetComponent<TextMeshProUGUI>();
        new_message.text_object.text = new_message.text;
        message_list.Add(new_message);

        //check if message matches voting options
        if (!users_voted.Contains(chatter) || chatter == "<Console>")
        {
            switch (message)
            {
                case "!vote ice-cavern":
                    {
                        vote_controller.vote(VoteOption.ICE_CAVERN);
                        new_message.text_object.color = new Color(0.0f, 1.0f, 1.0f);
                        users_voted.Add(chatter);

                        break;
                    }

                case "!vote underworld":
                    {
                        vote_controller.vote(VoteOption.UNDERWORLD);
                        new_message.text_object.color = new Color(1.0f, 0.55f, 0.0f);
                        users_voted.Add(chatter);

                        break;
                    }
            }
        }

        //scroll to the bottom
        Canvas.ForceUpdateCanvases();
        chat_scroll_rect.verticalNormalizedPosition = 0.0f;
    }
    public void onChatMessage(string chatter, string message)
    {
        sendMessageToChat(chatter, message);
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public TextMeshProUGUI text_object;
}

