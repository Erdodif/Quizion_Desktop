using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Admin
    {
        private Int64 id;
        //User.id => userId

      
        public long Id1 { get => id; set => id = value; }

        public Admin(int id)
        {
            this.id = id;
        
            
        }
    }
}
