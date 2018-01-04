using System;
using System.Threading;
using Login;
using Serwer2;

namespace Server
{
    internal class Program
    {
       
        private static void Main(string[] args)
        {


           
            var dB = new DB("Database.db");
            var commands = new Server_Commands(dB);
            var input = "";
            string IP;
            IP = Console.ReadLine();
            var connect = new Connection(IP, 3456);
            while (true)
            {
                connect.Connect();
                input = connect.Recive();
                //input = System.Console.ReadLine();
                //if (input == "exit") break;
                Console.WriteLine(commands.Do(input));
                connect.Respond(commands.Do(input));
            }
        }
    }
}