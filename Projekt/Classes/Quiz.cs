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
        private Int64 id;
        private string header;
        private string description;
        private Int64 active;
        private Int64 secondsPerQuiz;

        public long Id { get => id; set => id = value; }
        public string Header { get => header; set => header = value; }
        public string Description { get => description; set => description = value; }
        public long Active { get => active; set => active = value; }
        public long SecondsPerQuiz { get => secondsPerQuiz; set => secondsPerQuiz = value; }

        public Quiz(int id, string header,  string description,int active, int secondsPerQuiz)
        {
            this.id = id;
            this.header = header;
            this.description = description;
            this.active = active;
            this.secondsPerQuiz = secondsPerQuiz;
        }

        public Quiz(string q)
        {
            JsonSerializer.Create();
            JObject tartalom = JObject.Parse(q);
            IList<JToken> results = tartalom["data"].Children().ToList();


        }


        public override string ToString()
        {
            return $"{id,4} {header,-20} : {description, 20} {active,-20} {secondsPerQuiz}";
        }

    }
}
