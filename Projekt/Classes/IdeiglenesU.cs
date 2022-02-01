using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class IdeiglenesU
    {
        private string[] userId;
        private string[] password;

        public IdeiglenesU(string[] userId, string[] password)
        {
            this.userId = userId;
            this.password = password;
        }

        public string[] UserId { get => userId; set => userId = value; }
        public string[] Password { get => password; set => password = value; }
    }
}
