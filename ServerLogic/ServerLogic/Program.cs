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
            while (null == Connect)
            {
                Console.WriteLine("Please insert IP or type d for default localhost");
                Connect = ConnectionSetup(Console.ReadLine());
            }
        }

        public static Connection ConnectionSetup(string IP)
        {
            try
            {
                return IP == "d" ? new Connection("127.0.0.1", 3456) : new Connection(IP, 3456);
            }
            catch (Exception e)
            {
                Console.WriteLine("inserted value was not correct");
                return null;
            }
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