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
            Logic Logic = new Logic(dB);
            string input = "";
            while (input != "exit")
            {
                //connect.Connect();
                //input = connect.Recive();
                input = System.Console.ReadLine();
                System.Console.WriteLine(Logic.Do(input));
                System.Console.WriteLine("");
            }
            System.Console.ReadKey();
        }
    }
}
