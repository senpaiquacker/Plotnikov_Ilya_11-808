using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
namespace Client
{
    public class Program
    {
        public static TcpClient Client { get; private set; }
        public static NetworkStream Stream { get; private set; }
        private static int port = 8005;
        private static string address = "127.0.0.1";
        static void Main()
        {
            try
            {
                Client = new TcpClient(address, port);
                Stream = Client.GetStream();
                Application.Run(ClientWindows.MainWindow);
            }
            catch(Exception ex)
            {
                ClientWindows.MessageMaker(ex.Message);
            }
            finally
            {
                Client.Close();
            }
        }
    }
}
