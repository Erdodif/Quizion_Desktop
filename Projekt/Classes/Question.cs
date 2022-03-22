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
        private int quiz_id;
        private string content;
        private int point;

        public Question(int id, int quiz_id, string content, int point)
        {
            this.id = id;
            this.quiz_id = quiz_id;
            this.content = content;
            this.point = point;
        }

        public int Id { get => id; set => id = value; }
        public int Quiz_id { get => quiz_id; set => quiz_id = value; }
        public string Content { get => content; set => content = value; }
        public int Point { get => point; set => point = value; }

        public override string ToString()
        {
            return $"{id};{Quiz_id};{content};{point}";
        }
    }
}
