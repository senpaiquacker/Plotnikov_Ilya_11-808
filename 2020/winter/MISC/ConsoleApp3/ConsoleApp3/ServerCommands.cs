using Server.UserDatabase;
using System;
using System.Text;
using System.Linq;
namespace Server
{
    public class ServerCommands
    {
        internal static string Decrypt(string message)
        {
            var parts = message.Split("\\::\\");
            var command = parts[0];
            var parameters = parts[1];
            string answer = "FAILEDTODECRYPT";
            try
            {
                var answ = typeof(ServerCommands)
                .GetMethod(command, parameters
                .Split('\n')
                .Select(a => a.GetType())
                .ToArray())
                .Invoke(new ServerCommands(), parameters.Split('\n'));
                answer = (string)answ;
            }
            catch (Exception ex)
            {
                if (ex is MissingMethodException)
                {
                    Console.WriteLine("NO SUCH METHOD");
                }
            }
            return answer;
        }

        public string Download(string nickname)
        {
            var links = DbInteraction.Links(nickname);
            var builder = new StringBuilder();
            builder.Append("ListDownload\\::\\");
            foreach (var i in links)
            {
                builder.Append(i + "\n");
            }
            return builder.ToString();
        }
        public string Register(string nick, string mail, string pass)
        {
            try
            {
                DbInteraction.Register(nick, mail, pass);
                return "CheckRegister\\::\\SUCCESS";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "CheckRegister\\::\\FAILED";
            }
        }
        public string Login(string nickname, string pass)
        {
            return DbInteraction.IsValidAuthorization(nickname, pass) ?
                $"CheckLogin\\::\\SUCCESS\n{nickname}\n{pass}":
                $"CheckLogin\\::\\FAILED\n{nickname}\n{pass}";
        }
        public void LogOut(string nickname)
        {
            DbInteraction.LogOut(nickname);
        }
    }
}
