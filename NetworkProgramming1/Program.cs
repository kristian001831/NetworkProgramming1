using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text;

namespace NetworkProgramming1
{
    internal class Program
    {
        static int PORT = 23554;
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Are you listener (1) or sender? (2)");// TODO: also learn TCP and UDP
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.D1)
            {
                StartListener();
            }
            else
            {
                SendData();
            }

            Console.ReadKey();
        }

        private static void StartListener()
        {
            UdpClient listener = new UdpClient(PORT);
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORT);

            bool b = false;
            try
            {
                byte[] data = listener.Receive(ref ip); 
                Console.WriteLine(Encoding.ASCII.GetString(data));
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                listener.Close();// closes the udp 
            }
            
            Console.ReadKey();
        }

        private static void SendData()
        {
            UdpClient sender = new UdpClient(); // UdpClient can be send for both send and receive(a fire and forget
                                                // method, but needs the port, we want to work with)
            StringBuilder sb = new StringBuilder(15000000);// effizient for longer strings...

            string message = "";

            for (int i = 0; i < 500; i++)
            {
                sb.Append("Hello World!");
            }

            byte[] data = Encoding.ASCII.GetBytes(sb.ToString());

            try
            {
                sender.Send(data, data.Length, "localhost", PORT); // sending bytes; define how many and where
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sender.Close();
            }

            Console.ReadKey();
        }
    }
}