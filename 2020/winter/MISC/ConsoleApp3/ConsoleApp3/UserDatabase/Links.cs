using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.UserDatabase
{
    class Links
    {
        [Url, Required]
        public string Link { get; set; }
        public string NickName { get; set; }
        public Links()
        {

        }
        public Links(string link)
        {
            Link = link;
            NickName = string.Empty;
        }
    }
}
