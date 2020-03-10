using System;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using Server.UserDatabase;
namespace Server
{
    public class Server
    {
        static int port = 8005;
        static TcpListener listener;
        static string ip = "127.0.0.1";
        static void Main(string[] args)
        {
            DbInteraction.CheckLinks();
            try
            {
                listener = new TcpListener(IPAddress.Parse(ip), port);
                listener.Start();
                Console.WriteLine("OK");
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
    }
}
