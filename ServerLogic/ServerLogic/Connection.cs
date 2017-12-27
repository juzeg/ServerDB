using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Serwer2
{
    class Connection
    {
        public Socket s;
        TcpListener myList;
        // Constructor
        public Connection(string ip, int port)
        {
            IPAddress ipAd = IPAddress.Parse(ip);
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
            byte[] b = new byte[100];
            int k = s.Receive(b);
            string wynik = "";
            for (int i = 0; i < k; i++)
                wynik = wynik + Convert.ToChar(b[i]);
            return wynik;

        }
        /* Sending respond to client*/
        public void Respond(string respond)
        {
            ASCIIEncoding asen = new ASCIIEncoding();
            s.Send(asen.GetBytes(respond));
        }

        public int Requests()
        {
            return s.Available;
        }
    }
}
