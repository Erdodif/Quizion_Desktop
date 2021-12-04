using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class ApiAnswer<T>
    {
        bool error;
        string uzenet;
        List<T> adatok;

        public ApiAnswer(bool error, string uzenet, List<T> adatok)
        {
            this.error = error;
            this.uzenet = uzenet;
            this.adatok = adatok;
        }

        public bool Error { get => error; set => error = value; }
        public string Uzenet { get => uzenet; set => uzenet = value; }
        public List<T> Adatok { get => adatok; set => adatok = value; }
    }
}
