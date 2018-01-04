using System;
using System.Net;
using Login;
using ServerLogic;
using Serwer2;

namespace Server
{
    public class Program

    {
        private static object db;

        public static Connection Connect { get; set; }


        private static void Main(string[] args)
        {
            Initialise();


            while (Core_functionality()) ;
        }

        private static void Initialise()
        {
            db = new DataBase("DataBase");
             Connect = new Connection(3456);
        }

      

        private static bool Core_functionality()
        {
            Connect.Connect();
            var input = Connect.Recive();
            input = Console.ReadLine();

            Console.WriteLine(Commands.Do(input));
            Connect.Respond(Commands.Do(input));

            if (input == "exit") return false;
            return true;
        }
    }
}