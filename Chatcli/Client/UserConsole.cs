using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace Chatcli
{
    class UserConsole
    {
        public bool sessionActive { get; set; }
        public string Username { get; private set; }
        private string currentMessage = "";
        private ConnectionManager connectionManager = new ConnectionManager();
        private List<int> cursorInputPosition = new List<int>();
        public void callback(object state)
        {
            int y = Console.CursorTop;
            Console.SetCursorPosition(3, cursorInputPosition[1]);
            Console.Write(DateTime.Now);
            Console.SetCursorPosition(cursorInputPosition[0],y);
        }
        public UserConsole(string username)
        {
            while (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("You can't not have a username!");
                username = Console.ReadLine();
            }
            Username = username;
        }
        public void displayLastNMessages(int n=1)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            int i = 5;
            foreach (var item in requestMessages(n)) //implement restrictions on border
            {
                Console.SetCursorPosition(3, y-i);
                Console.Write(item.Timestamp.ToString() + " " + item.Sender + ": " + item.Content);
                i++;
                
            }
            Console.SetCursorPosition(x, y);
        }
        public List<ChatMessage> requestMessages(int n=1)
        {
            List<ChatMessage> msgs= new List<ChatMessage>();
            msgs.Add(new ChatMessage("putaind", "rws"));
            msgs.Add(new ChatMessage("sdsd", "rws"));
            msgs.Add(new ChatMessage("sdsd", "rws"));
            msgs.Add(new ChatMessage("sdsd", "rws"));
            msgs.Add(new ChatMessage("sdsd", "rws"));// test
            msgs.Add(new ChatMessage("sdsd", "rws"));
            return msgs;
        }
        public void DeleteCharacters(int n = 1)
        {
            //add mindfullness ??
            for (int i = 0; i < n; i++)
            {
                Console.Write("\x1B[1D");
                Console.Write("\x1B[1P");

            }
            currentMessage = currentMessage.Remove(currentMessage.Length-n);
        }
        public void Begin()

        {

            ConsoleKeyInfo input;
            string stringKey;
            int dateTimeLength = 19;
            Console.CursorTop = Console.WindowTop + Console.WindowHeight - 3;
            Console.SetCursorPosition(dateTimeLength + 3, Console.CursorTop);
            int mindfullness = 0;
            
            //displayLastNMessages(20);
            
            

            Console.Write(" " + Username + ": ");

            
            cursorInputPosition.Add(Console.CursorLeft);
            cursorInputPosition.Add(Console.CursorTop);
            Timer timer = new Timer(callback, null , TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

            
            while ((input = Console.ReadKey(true)).Key != ConsoleKey.Q) // might add arrows
            {
                stringKey = input.KeyChar.ToString();

                if (input.Key == ConsoleKey.Backspace)
                {
                    if (Console.CursorLeft > (6+Username.Length+dateTimeLength))
                    {
                        DeleteCharacters();
                    }
                }
                else if(input.Key == ConsoleKey.Enter)
                {
                    if ((currentMessage.Length > 3) & (currentMessage.Remove(3) == "/dm"))
                    {
                        //check length
                        var msgInfo = currentMessage.Substring(4).Split(); // split into username & message

                    }
                    else
                    {
                        DeleteCharacters(currentMessage.Length); // bugged: deletes only current line
                        SendMessage(currentMessage);
                    }
                    currentMessage = "";
                    Console.SetCursorPosition(6+Username.Length+dateTimeLength, cursorInputPosition[1]);
                }
                else if(input.Key == ConsoleKey.PageDown)
                {
                    displayLastNMessages(6);
                    ScrollChat(false);
                }
                else if(input.Key == ConsoleKey.PageUp)
                {
                    ScrollChat(true);
                }
                else if(input.Key == ConsoleKey.Spacebar)
                {
                    UserInput(" ");
                }
                else
                {
                    
                    if (Console.CursorLeft == Console.WindowWidth - 4)
                    {
                        Console.SetCursorPosition(6 + Username.Length + dateTimeLength, Console.CursorTop + 1); // figure a way to add this to queued thrashing
                        mindfullness++;
                    }
                    UserInput(stringKey);
                }
                cursorInputPosition[0] = Console.CursorLeft;
                
            }
        }
        public void UserInput(string message) // what if it's a newline
        {
            currentMessage += message;
            Console.Write(message); 
        }
        public void SendMessage(string message, string recipient = "global")
        {
            if (!String.IsNullOrEmpty(message))
            {
                connectionManager.sendMessage(message, recipient);
            }
        }
        
        public void ScrollChat(bool direction)
        {
            ConsoleKey key;
 
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.End)
            {
                if (key == ConsoleKey.PageUp)
                {
                    connectionManager.requestMessage(true);
                    //also do move
                }
                else if (key == ConsoleKey.PageDown)
                {
                    connectionManager.requestMessage(false);
                    //launch request
                    //also do move
                }
            }
            //make request to get one line
            // then 
        }
        // need a separate thread which monitores new messages
    }
}
