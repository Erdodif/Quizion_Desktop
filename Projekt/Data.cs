using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Projekt
{
    class Data
    {
        private int code;
        private String content;

        public Data(int code, String content)
        {
            this.code = code;
            this.content = content;
        }
        async static void Call(String url, Func<String,bool> handler) {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("charset", "utf8");
                string content= await client.GetStringAsync(url);
                handler(content);
            }
        }
        //NAGYON FONTOS!!!
        static void init()
        {
            Func<String, bool> handler = str => {
                Answer valasz = new Answer(str);
                return true;
            };
            Call("link", handler);
        }
    }
}
