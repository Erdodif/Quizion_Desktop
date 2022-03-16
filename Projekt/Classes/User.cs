using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class User
    {
        private int id;
        private string name;
        private string email;
        private string  password;
        private int xp;

        public User(int id, string name, string email, string password, int xp)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.xp = xp;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public int Xp { get => xp; set => xp = value; }

        public override string ToString()
        {
            return ($"{this.id};{this.name};{this.xp}");
        }


    }
}
