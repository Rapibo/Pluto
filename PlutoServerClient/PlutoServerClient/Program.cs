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

            Console.WriteLine("A client connected.");
            var stream = client.GetStream();
            
            if (!client.Client.Poll(0, SelectMode.SelectRead))
            {
                while (true)
                {
                    var bytes = new byte[client.Available];

                    stream.Read(bytes, 0, bytes.Length);

                    var data = Encoding.UTF8.GetString(bytes);


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
            }
            else
            {
                Console.WriteLine("{0} {1}", DateTime.Now.ToString("HH:mm:ss"), "DISCONNECTED");
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
