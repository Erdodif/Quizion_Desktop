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
        private int userId;

        public Admin(int id, int userId)
        {
            this.id = id;
            this.userId = userId;
        }

        public int Id { get => id; set => id = value; }
        public int UserId { get => userId; set => userId = value; }

        public override string ToString()
        {
            return $"{id};{userId}";
        }
    }
}
