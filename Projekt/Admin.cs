using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Admin:User
    {
        private Int64 id;

      
        public long Id1 { get => id; set => id = value; }

        public Admin(int id) : base(id)
        {
            this.id = id;
        
            
        }
    }
}
