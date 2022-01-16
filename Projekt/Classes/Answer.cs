using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Projekt
{
    class Answer
    {
        private int id;
        private int questionId;
        private string content;
        private bool isRight;

        public int Id { get => id; set => id = value; }
        public int QuestionId { get => questionId; set => questionId = value; }
        public string Content { get => content; set => content = value; }
        public bool IsRight { get => isRight; set => isRight = value; }


        public Answer(int id, int questionId, string content, bool isRight)
        {
            this.id = id;
            this.questionId = questionId;
            this.content = content;
            this.isRight = isRight;
        }

        /*public Answer(string q)
        {
            JsonSerializer.Create();
            JObject tartalom = JObject.Parse(q);
            IList<JToken> results = tartalom["data"].Children().ToList();


        }
        */

        public override string ToString()
        {
            return $"{id,3} {questionId, 4} , {content,25} {isRight,4}";
        }
    }
}
