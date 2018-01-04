using System;
using Login;
using ServerLogic;
using Serwer2;

namespace Server
{
    internal class Program

    {
        private static object db;

        public static Connection connect { get; set; }


        private static void Main(string[] args)
        {
            Initialise();


            while (Core_functionality()) ;
        }

        private static void Initialise()
        {
            db = new DataBase("DataBase");


            try
            {
                string IP = "dupa";
                if (IP == "d")
                    connect = new Connection("127.0.0.1", 3456);
                else
                    connect = new Connection(IP, 3456);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("error parsing ipS adress");
                throw;
            }
        }

        private static bool Core_functionality()
        {
            connect.Connect();
            var input = connect.Recive();
            input = Console.ReadLine();

            Console.WriteLine(Commands.Do(input));
            connect.Respond(Commands.Do(input));

            if (input == "exit") return false;
            return true;
        }
    }
}