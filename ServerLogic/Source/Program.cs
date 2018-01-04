#region

using System;

#endregion

namespace ServerLogic.Source
{
    public static class Program

    {
        private static object _db;
        private static Connection Connect { get; set; }


        private static void Main(string[] args)
        {
            Initialise();

            while (true)
            {
                Console.WriteLine("executing core functionality.");
                Core_functionality();
            }
        }

        private static void Initialise()
        {
            Console.WriteLine("initalisation in progress...");
            _db = new DataBase("DataBase.db");

            Connect = new Connection(3456);
            Console.WriteLine("initialisation complete.");
        }


        private static bool Core_functionality()
        {
            Connect.Connect();
            Console.WriteLine("Connection estabilished");
            var input = Connect.Recive();
            Console.Clear();
            Console.WriteLine("Awaiting Commands.");

            input = Console.ReadLine();

            Console.WriteLine(Commands.Do(input));
            Connect.Respond(Commands.Do(input));
            Console.WriteLine("happened");
            if (input != "exit") return false;
            return true;
        }
    }
}