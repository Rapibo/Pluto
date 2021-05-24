using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlutoServerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);
            server.Start();
            Console.WriteLine("Server started {0}{1}", DateTime.Now.ToString("dd MMM HH:mm:ss"), Environment.NewLine);
            var client = server.AcceptTcpClient();

            Console.WriteLine("{0} A client connected.", DateTime.Now.ToString("dd MMM HH:mm:ss"));
            var stream = client.GetStream();

            

            //if (client.Client.Poll(0, SelectMode.SelectRead))
            //{
            //while (true)
            //{
            //var bytes = new byte[client.Available];

            //stream.Read(bytes, 0, bytes.Length);

            //var data = Encoding.UTF8.GetString(bytes);


            //if (data != "")
            //{
            //    if (Regex.IsMatch(data, "^GET", RegexOptions.IgnoreCase))
            //    {
            //        Console.WriteLine("=====Handshaking from client=====\n{0}", data);
            //    }
            //    else
            //    {
            //        Console.WriteLine("{0} {1}", DateTime.Now.ToString("HH:mm:ss"), data);
            //    }
            //}


            //}

            //var buff = new byte[1];
            //if (client.Client.Receive(buff, SocketFlags.Peek) == 0)
            //{
            //    if (client.Connected)
            //    {
            //        stream = client.GetStream();
            //    }
            //    // Client disconnected

            //}

            //}
            //else
            //{
            //    Console.WriteLine("{0} {1}", DateTime.Now.ToString("HH:mm:ss"), "DISCONNECTED");
            //}
            while (client.Connected)
            {
                if (client.Available > 0)
                {
                    Console.WriteLine("{0} Antal anslutna: {1}", DateTime.Now.ToString("HH:mm:ss"), client.Available);
                }
                else
                {
                    Console.WriteLine("{0} Inga anslutna", DateTime.Now.ToString("HH:mm:ss"));
                }

            }
            
            

        }

        static bool IsConnected(Socket _nSocket)
        {
            if (_nSocket.Connected)
            {
                if ((_nSocket.Poll(0, SelectMode.SelectWrite)) && (!_nSocket.Poll(0, SelectMode.SelectError)))
                {
                    byte[] buffer = new byte[1];
                    if (_nSocket.Receive(buffer, SocketFlags.Peek) == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


    }

    static class SocketExtensions
    {
        public static bool IsConnected(this Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }
    }
}
