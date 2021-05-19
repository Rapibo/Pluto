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

            
            while (client.Available < 3)
            {
                // wait for enough bytes to be available
            }

            var bytes = new Byte[client.Available];

            stream.Read(bytes, 0, bytes.Length);

            var data = Encoding.UTF8.GetString(bytes);

            
        }
    }
}
