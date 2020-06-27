using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Chatcli
{
    [System.Runtime.InteropServices.ComVisible(true)]
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Type 'S' to be the server, any other key to be a pleb");
            //if (Console.ReadKey(true).Key == ConsoleKey.S); //we do partay
            //else {
                Console.WriteLine("choose a username: ");
            //while (true) Console.Read()
                UserConsole userConsole = new UserConsole(Console.ReadLine());
                userConsole.Begin();
            
           // }
        }
        //KeyPressEventHandler()
        
        
    }
}