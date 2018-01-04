
using System;
using Login;
using Serwer2;

namespace Server
{
    internal class Program

    {
        private static object db;

        public Connection connection { get; set; }


        private static void Main(string[] args)
        {
            Initialise();


            while (true) Core_functionality();
        }

        private static void Initialise()
        {
            
           
            db = new DB.("DataBase");

            var commands = new Server_Commands(dB);
            var input = "";
            string IP;

            var connection = new SetUpconnection();
            try
            {
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

        private static void Core_functionality()
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