using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int max_messages = 25;

    [SerializeField] List<Message> message_list = new List<Message>();

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            //sendMessageToChat
        }
    }

    public void sendMessageToChat(string text)
    {
        if (message_list.Count >= max_messages)
        {
            message_list.Remove(message_list[0]);
        }

        Message new_message = new Message();

        new_message.text = text;

        message_list.Add(new_message);
    }
}

[System.Serializable]
public class Message
{
    public string text;
}

