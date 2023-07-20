using System;

namespace Chatcli
{
    class ChatMessage
    {
        public string Sender { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Content { get; private set; }
        public string Recipient { get; private set; }
        public ChatMessage(string sender, string content, string recipient="global")
        {
            Sender = sender;
            Content = content;
            Timestamp = DateTime.Now;
            Recipient = recipient;
        }

    }
}
