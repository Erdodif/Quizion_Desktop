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
        private int secondsPerQuiz;

        public int Id { get => id; set => id = value; }
        public string Header { get => header; set => header = value; }
        public string Description { get => description; set => description = value; }
        public int Active { get => active; set => active = value; }
        public int SecondsPerQuiz { get => secondsPerQuiz; set => secondsPerQuiz = value; }

        public Quiz(int id, string header,  string description,int active, int secondsPerQuiz)
        {
            this.id = id;
            this.header = header;
            this.description = description;
            this.active = active;
            this.secondsPerQuiz = secondsPerQuiz;
        }

       /* public Quiz(string q)
        {
            JsonSerializer.Create();
            JObject tartalom = JObject.Parse(q);
            IList<JToken> results = tartalom["data"].Children().ToList();


        }
       */


        public override string ToString()
        {
            return $"{id};{header};{description};{active};{secondsPerQuiz}";
        }

    }
}
