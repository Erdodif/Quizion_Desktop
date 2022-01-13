using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Projekt
{
    class Question
    {
        private Int64 id;
        private Int64 quizId;
        private string content;
        private Int64 point;

        public long Id { get => id; set => id = value; }
        public long QuizId { get => quizId; set => quizId = value; }
        public string Content { get => content; set => content = value; }
       
        public long Point { get => point; set => point = value; }
       

        public Question(int id, int quizId, string content, int point)
        {
            this.id = id;
            this.quizId = quizId;
            this.content = content;
            this.point = point;
        }

        public Question(string q)
        {
            JsonSerializer.Create();
            JObject tartalom = JObject.Parse(q);
            IList<JToken> results = tartalom["data"].Children().ToList();
        }

        public override string ToString()
        {
            return $"{id,4} {quizId,4} , {content,-20} {point}";
        }
    }
}
