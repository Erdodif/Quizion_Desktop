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
        private int id;
        private int quizId;
        private string content;
        private int point;

        public int Id { get => id; set => id = value; }
        public int QuizId { get => quizId; set => quizId = value; }
        public string Content { get => content; set => content = value; }
       
        public int Point { get => point; set => point = value; }
       

        public Question(int id, int quizId, string content, int point)
        {
            this.id = id;
            this.quizId = quizId;
            this.content = content;
            this.point = point;
        }

        /*public Question(string q)
        {
            JsonSerializer.Create();
            JObject tartalom = JObject.Parse(q);
            IList<JToken> results = tartalom["data"].Children().ToList();
        }
        */

        public override string ToString()
        {
            return $"{id,4} {quizId,4} , {content,25} {point,5}";
        }
    }
}
