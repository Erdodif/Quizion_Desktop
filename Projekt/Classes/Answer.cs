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
        private int question_id;
        private string content;
        private bool is_right;

        public Answer(int id, int question_id, string content, bool is_right)
        {
            this.id = id;
            this.question_id = question_id;
            this.content = content;
            this.is_right = is_right;
        }

        public int Id { get => id; set => id = value; }
        public int Question_id { get => question_id; set => question_id = value; }
        public string Content { get => content; set => content = value; }
        public bool Is_right { get => is_right; set => is_right = value; }

        public override string ToString()
        {
            return $"{id};{question_id};{content,25};{is_right}";
        }
    }
}
