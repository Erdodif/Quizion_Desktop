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
        private string content;
        private Int64 rightAnswers;
        private Int64 point;

        public long Id { get => id; set => id = value; }
        public string Content { get => content; set => content = value; }
        public long RightAnswers { get => rightAnswers; set => rightAnswers = value; }
        public long Point { get => point; set => point = value; }

        public Quiz(int id, string content, int rightAnswers, int point)
        {
            this.id = id;
            this.content = content;
            this.rightAnswers = rightAnswers;
            this.point = point;
        }

        public Quiz(string q)
        {
            JsonSerializer.Create();
            JObject tartalom = JObject.Parse(q);
            IList<JToken> results = tartalom["data"].Children().ToList();


        }


        public override string ToString()
        {
            return $"{id,4} {content,-20} {rightAnswers,-20} {point}";
        }

    }
}
