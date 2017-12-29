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
            //Connection connect = new Connection("77.55.219.19" , 3456);
            DB dB = new DB("Database.db");
            Commands commands = new Commands(dB);
            string input = "";
            while (true)
            {
                //connect.Connect();
                //input = connect.Recive();
                input = System.Console.ReadLine();
                if (input == "exit") break;
                else System.Console.WriteLine(commands.Do(input));
                System.Console.WriteLine("");
            }
        }
    }
}
