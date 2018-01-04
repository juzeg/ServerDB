using System;
using System.Threading;
using Serwer2;

namespace Server
{
    internal class SetUpconnection
    {
        public  SetUpconnection()
        {
            ConnectonCreator();
        }

        private Connection ConnectonCreator()
        {
            string Consoleinput;
            Console.WriteLine("Please insert target IP adress or type d for dafault (localhost)");
            Consoleinput = Console.ReadLine();
            return null;
        }

      
    }
}