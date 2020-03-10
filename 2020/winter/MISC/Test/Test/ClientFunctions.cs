using System;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
namespace Client
{
    public static class ClientFunctions
    {
        private static string cookiePath
        {
            get
            {
                if (!File.Exists(Directory.GetCurrentDirectory() + "\\cookie.txt"))
                    File
                        .WriteAllText(Directory
                            .GetCurrentDirectory() + "\\cookie.txt", "");
                return Directory.GetCurrentDirectory() + "\\cookie.txt";
            }
        } 
        public static string cookieInfo
        {
            get
            {
                return File.ReadAllText(cookiePath);
            }
        }
        internal static void Decrypt(string message)
        {
            var parts = message.Split(new[] { "\\::\\" },StringSplitOptions.RemoveEmptyEntries);
            var command = parts[0];
            var parameters = parts[1].Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if(command == "ListDownload")
                    typeof(ClientFunctions)
                    .GetMethod(command)
                    .Invoke(null, new[] {parameters});
                else
                    typeof(ClientFunctions)
                    .GetMethod(command)
                    .Invoke(null, parameters);
            }
            catch (Exception ex)
            {
                if (ex is MissingMethodException)
                {
                    ClientWindows.MessageMaker("NO SUCH METHOD");
                }
            }
        }
        public static void Send(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            Program.Stream.Write(data, 0, data.Length);
            data = new byte[256];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Program.Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Program.Stream.DataAvailable);
            Decrypt(builder.ToString());
        }
        public static string Login(string nickname, string password)
        {
            return $"Login\\::\\{nickname}\n{password}";
        }
        public static string Register(string mail, string nickname, string password)
        {
            return $"Register\\::\\{mail}\n{nickname}\n{password}";
        }
        public static string DownloadLinks(string nickname)
        {
            return $"Download\\::\\{nickname}";
        }
        public static string LogOut(string nickname)
        {
            return $"LogOut\\::\\{nickname}";
        }
        public static void Download(params string[] links)
        {
            var client = new WebClient();
            var count = 0;
            foreach(var i in links)
            {
                try
                {
                    client.DownloadFile(i, i.Split('/').Last());
                    count++;
                }
                catch(Exception ex)
                {
                    if(!(ex is WebException))
                        ClientWindows.MessageMaker(ex.Message);
                }
            }
            if (count < links.Count())
                ClientWindows.MessageMaker("Some of the Files weren't downloaded");
        }
        public static void ListDownload(params string[] links)
        {
            ((MainWindow)ClientWindows.MainWindow).LinkList = links.ToList();
        }
        public static void CheckDownload(string message)
        {
            switch(message)
            {
                case "SUCCESS":
                    //UserInterface.
                    break;
                case "FAILED":
                    ClientWindows.MessageMaker("Registration Failed. Try Again");
                    break;
                default:
                    throw new ArgumentException("Unrecognizable reaction");
            }
        }
        public static void CheckLogin(string message, string nickname, string password)
        {
            switch(message)
            {
                case "SUCCESS":
                    if((ClientWindows.CurrentWindow as LoginWindow).CookieEnabled)
                        SaveCookie(nickname, password);
                    ClientWindows.CurrentWindow = ClientWindows.MainWindow;
                    ((MainWindow)ClientWindows.CurrentWindow).LoggedNickname = nickname;
                    break;
                case "FAILED":
                    ClientWindows.MessageMaker("Login Failed. Try Again");
                    break;
                default:
                    throw new ArgumentException("Unrecognizable reaction");
            }
        }
        public static bool IsDownloaded(string file)
        {
            return File.Exists(Directory.GetCurrentDirectory() + "\\" + file);
        }
        public static void CheckRegister(string message)
        {
            switch(message)
            {
                case "SUCCESS":
                    ClientWindows.CurrentWindow = new LoginWindow();
                    ClientWindows
                        .MessageMaker("Success! Now you can log in, using these login and password");
                    break;
                case "FAILED":
                    ClientWindows.MessageMaker("Registration Failed. Try Again");
                    break;
                default:
                    throw new ArgumentException("Unrecognizable reaction");
            }
        }
        private static void SaveCookie(string nickname, string password)
        {
            File.WriteAllText(cookiePath, 
                nickname + "\n" + password);
        }
    }
}
