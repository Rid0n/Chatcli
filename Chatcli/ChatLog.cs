using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Chatcli
{
    class ChatLog
    {
        public List<ChatMessage> Log = new List<ChatMessage>();
        public List<ChatMessage> GetLastNMessages(int n=1)
        {
            return Log.TakeLast(n).ToList();
        }

    }
}
