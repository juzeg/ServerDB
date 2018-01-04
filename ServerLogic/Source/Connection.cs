#region

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

#endregion

namespace ServerLogic.Source
{
    public class Connection
    {
        private readonly TcpListener _myList;

        private Socket _s;


        public Connection(int port)
        {
            var ipAd = IPAddress.Parse(GetLocalIpAddress());

            /* Initializes the Listener */
            _myList = new TcpListener(ipAd, port);
            /* Start Listeneting at the specified port */
            _myList.Start();
        }

        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
                return ip.ToString();
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


        /*Creating connection*/
        public void Connect()
        {
            Console.WriteLine("connecting...");
            /* Accept connection*/
            try
            {
                _s = _myList.AcceptSocket();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine("connected");
        }

        /* Reciving messeage from client*/
        public string Recive()
        {
            var b = new byte[100];
            var k = _s.Receive(b);
            var wynik = "";
            for (var i = 0; i < k; i++)
                wynik = wynik + Convert.ToChar(b[i]);
            return wynik;
        }

        /* Sending respond to client*/
        public void Respond(string respond)
        {
            var asen = new ASCIIEncoding();
            _s.Send(asen.GetBytes(respond));
        }

        public int Requests()
        {
            return _s.Available;
        }
    }
}