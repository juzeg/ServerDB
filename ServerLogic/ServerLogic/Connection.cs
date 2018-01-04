using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Serwer2
{
    public class Connection
    {
        private readonly TcpListener myList;

        public Socket s;

        public string IP;
        // Constructor
        public Connection( int port)
        {
            
            var ipAd = IPAddress.Parse(GetLocalIPAddress());
           
            /* Initializes the Listener */
            myList = new TcpListener(ipAd, port);
            /* Start Listeneting at the specified port */
            myList.Start();
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


        /*Creating connection*/
        public void Connect()
        {
            /* Accept connection*/
            s = myList.AcceptSocket();
        }

        /* Reciving messeage from client*/
        public string Recive()
        {
            var b = new byte[100];
            var k = s.Receive(b);
            var wynik = "";
            for (var i = 0; i < k; i++)
                wynik = wynik + Convert.ToChar(b[i]);
            return wynik;
        }

        /* Sending respond to client*/
        public void Respond(string respond)
        {
            var asen = new ASCIIEncoding();
            s.Send(asen.GetBytes(respond));
        }

        public int Requests()
        {
            return s.Available;
        }
    }
}