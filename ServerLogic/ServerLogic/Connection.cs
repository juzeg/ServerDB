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
        public Connection(string ip, int port)
        {
            var ipAd = IPAddress.Parse(ip);
            IP = ip;
            /* Initializes the Listener */
            myList = new TcpListener(ipAd, port);
            /* Start Listeneting at the specified port */
            myList.Start();
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