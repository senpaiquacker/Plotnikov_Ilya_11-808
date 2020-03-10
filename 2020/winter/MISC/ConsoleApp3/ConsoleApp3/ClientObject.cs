using System;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class ClientObject
    {
        public TcpClient Client;
        public ClientObject(TcpClient connection)
        {
            Client = connection;
        }
        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                stream = Client.GetStream();
                byte[] data = new byte[1024];
                while(true)
                {
                    var builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    Console.WriteLine(builder.ToString());
                    string message = ServerCommands.Decrypt(builder.ToString());
                    data = Encoding.Unicode.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if(!Client.Connected)
                {
                    if (stream != null)
                        stream.Close();
                    if (Client != null)
                        Client.Close();
                }
            }
        }
    }
}
