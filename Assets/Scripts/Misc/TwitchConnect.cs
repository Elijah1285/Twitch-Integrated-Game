using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Net.Sockets;
using System.IO;

public class TwitchConnect : MonoBehaviour
{
    public UnityEvent<string, string> on_chat_message;

    TcpClient twitch;
    StreamReader reader;
    StreamWriter writer;

    const string URL = "irc.chat.twitch.tv";
    const int PORT = 6667;

    string user = "elijah12853";
    string OAuth = "oauth:5i5vgk3dwkb2sbq1coew87jnp5j3sv";
    string channel = "elijah12853";

    float ping_counter = 0.0f;

    void connectToTwitch()
    {
        twitch = new TcpClient(URL, PORT);
        reader = new StreamReader(twitch.GetStream());
        writer = new StreamWriter(twitch.GetStream());

        writer.WriteLine("PASS " + OAuth);
        writer.WriteLine("NICK " + user.ToLower());
        writer.WriteLine("JOIN #" + channel.ToLower());
        writer.Flush();
    }

    void Awake()
    {
        connectToTwitch();
    }

    void Update()
    {
        ping_counter += Time.deltaTime;

        if (ping_counter > 60.0f)
        {
            writer.WriteLine("PING " + URL);
            writer.Flush();
            ping_counter = 0.0f;
        }

        if (!twitch.Connected)
        {
            connectToTwitch();
        }

        if (twitch.Available > 0)
        {
            string message = reader.ReadLine();

            if (message.Contains("PRIVMSG"))
            {
                int split_point = message.IndexOf("!");
                string chatter = message.Substring(1, split_point - 1);

                split_point = message.IndexOf(":", 1);
                string msg = message.Substring(split_point + 1);

                on_chat_message?.Invoke(chatter, msg);
            }

            print(message);
        }
    }
}
