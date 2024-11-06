using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatController : MonoBehaviour
{
    public int max_messages = 25;

    public GameObject chat_panel;
    public GameObject text_object;
    public TMP_InputField chat_box;

    [SerializeField] List<Message> message_list = new List<Message>();

    void Start()
    {
        
    }

    void Update()
    {
        if (chat_box.text != "")
        {
            if (Input.GetButtonDown("Submit"))
            {
                sendMessageToChat(chat_box.text);
                chat_box.text = "";
            }
        }
        else
        {
            if (!chat_box.isFocused && Input.GetButtonDown("Submit"))
            {
                chat_box.ActivateInputField();
            }
        }
    }

    public void sendMessageToChat(string text)
    {
        if (message_list.Count >= max_messages)
        {
            Destroy(message_list[0].text_object.gameObject);
            message_list.Remove(message_list[0]);
        }

        Message new_message = new Message();

        new_message.text = text;

        GameObject new_text = Instantiate(text_object, chat_panel.transform);

        new_message.text_object = new_text.GetComponent<TextMeshProUGUI>();

        new_message.text_object.text = new_message.text;

        message_list.Add(new_message);
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public TextMeshProUGUI text_object;
}

