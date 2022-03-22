using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Projekt
{
    class Quiz
    {
        private int id;
        private string header;
        private string description;
        private int active;
        private int seconds_per_quiz;

        public Quiz(int id, string header, string description, int active, int seconds_per_quiz)
        {
            this.id = id;
            this.header = header;
            this.description = description;
            this.active = active;
            this.seconds_per_quiz = seconds_per_quiz;
        }

        public int Id { get => id; set => id = value; }
        public string Header { get => header; set => header = value; }
        public string Description { get => description; set => description = value; }
        public int Active { get => active; set => active = value; }
        public int Seconds_per_quiz { get => seconds_per_quiz; set => seconds_per_quiz = value; }

        public override string ToString()
        {
            return $"{id};{header};{description};{active};{seconds_per_quiz}";
        }

    }
}
