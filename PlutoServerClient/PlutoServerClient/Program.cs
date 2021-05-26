using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SimpleTCP;

namespace PlutoServerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new SimpleTcpServer();
            try
            {
                server.Start(IPAddress.Parse("127.0.0.1"), 80);
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                
            }
            

            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
            server.DataReceived += Server_DataReceived;
            
            Console.ReadKey();
            
        }

        private static void Server_DataReceived(object sender, Message e)
        {
            var data = Encoding.UTF8.GetString(e.Data);


            if (data != "")
            {
                if (Regex.IsMatch(data, "^GET", RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("=====Handshaking from client=====\n{0}", data);
                }
                else
                {
                    Console.WriteLine("{0} {1}", DateTime.Now.ToString("HH:mm:ss"), data);
                }
            }
        }

        private static void Server_ClientDisconnected(object sender, TcpClient e)
        {
            Console.WriteLine("{0} {1} Disconnected", DateTime.Now.ToString("HH:mm:ss"), e.Client.RemoteEndPoint.Serialize());
        }

        private static void Server_ClientConnected(object sender, TcpClient e)
        {
            Console.WriteLine("{0} {1} Connected", DateTime.Now.ToString("HH:mm:ss"), e.Client.RemoteEndPoint.Serialize());
        }

        


    }
    
}
