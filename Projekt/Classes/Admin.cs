using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Admin
    {
        private int id;
        private int user_id;

        public Admin(int id, int user_id)
        {
            this.id = id;
            this.user_id = user_id;
        }

        public int Id { get => id; set => id = value; }
        public int User_id { get => user_id; set => user_id = value; }

        public override string ToString()
        {
            return $"{id};{user_id}";
        }
    }
}
