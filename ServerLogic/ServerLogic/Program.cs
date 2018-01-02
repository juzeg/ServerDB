using Login;
using Serwer2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            
            DB dB = new DB("Database.db");
            Commands commands = new Commands(dB);
            string input = "";
            string IP;
            IP = Console.ReadLine();
            Connection connect = new Connection(IP, 3456);
            while (true)
            {
                
                connect.Connect();
                input = connect.Recive();
                //input = System.Console.ReadLine();
                //if (input == "exit") break;
                System.Console.WriteLine(commands.Do(input));
                connect.Respond(commands.Do(input));
            }
        }
    }
}
