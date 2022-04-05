using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Projekt
{
    [TestFixture]
    class TestOfQuizion
    {
        

         [TestCase]
         public void BeginTest()
        {
            ColorsOfQuizion c = new ColorsOfQuizion();
            Assert.IsFalse(c.Black == c.OnSecondary);
        }


        [TestCase]
        public async Task TokenTest()
        {
            HttpResponseMessage response = await Login();
            string token = "4a337678464a70633031747072757964426b46786b645244446952364871695237466555356166453359476d3475523635526b4d66775a484d7539534c6d5234";
            string token2 = await Token();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string reply = await client.GetStringAsync("http://127.0.0.1:8000/admin/quizzes");
            List<Quiz> quiz = JsonConvert.DeserializeObject<List<Quiz>>(reply);
            Assert.AreNotEqual(token2, token);
        }

        [TestCase]
        public async Task QuestionTest()
        {
            string token = "4a337678464a70633031747072757964426b46786b645244446952364871695237466555356166453359476d3475523635526b4d66775a484d7539534c6d5234";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string reply = await client.GetStringAsync("http://127.0.0.1:8000/admin/questions");
            List<Question> question = JsonConvert.DeserializeObject<List<Question>>(reply);
            Assert.IsTrue(question is List<Question>);
        }

       

        [TestCase]
        public async Task LoginTest()
        {
            HttpResponseMessage response = await Login();
            Assert.IsTrue(Convert.ToInt32(response.StatusCode) == 201);
        }

        [TestCase]
        public async Task InsertTest()
        {
            HttpResponseMessage response = await Login();
            HttpClient client2 = new HttpClient();
            client2.BaseAddress = new Uri("http://127.0.0.1:8000");
            string token = await Token();
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            JObject jObject2 = new JObject();
            jObject2.Add("header", "Következő kvíz");
            jObject2.Add("description", "Következő kvíz leírása");
            string contente = JsonConvert.SerializeObject(jObject2);
            var stringContente = new StringContent(contente, Encoding.UTF8, "application/json");
            var response2 = await client2.PostAsync("/admin/quizzes/", stringContente);
            Assert.IsTrue(Convert.ToInt32(response2.StatusCode) == 201);
        }

        [TestCase]
        public async Task ErrorInsertTest()
        {
            HttpResponseMessage response = await Login();
            HttpClient client2 = new HttpClient();
            client2.BaseAddress = new Uri("http://127.0.0.1:8000");
            string token = await Token();
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            JObject jObject2 = new JObject();
            jObject2.Add("header", "1111");
            jObject2.Add("description", "Következő kvíz leírása");
            string contente = JsonConvert.SerializeObject(jObject2);
            var stringContente = new StringContent(contente, Encoding.UTF8, "application/json");
            var response2 = await client2.PostAsync("/admin/quizzes/", stringContente);
            Assert.IsTrue(Convert.ToInt32(response2.StatusCode) == 400);
        }

        [TestCase]
        public async Task ErrorLoginNoInputsTest()
        {
            HttpResponseMessage response = await ErrorLoginEmptyInputs();
            Assert.IsTrue(Convert.ToInt32(response.StatusCode) == 500);
        }


        private async Task<HttpResponseMessage> Login()
        {
            HttpClient client = new HttpClient();
            string url = "/api/users/login";
            client.BaseAddress = new Uri("http://127.0.0.1:8000");
            JObject jObject = new JObject();
            jObject.Add("userID", "somass");
            jObject.Add("password", "123456789");
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);
            string token = response.Content.ReadAsStringAsync().Result;
            string[] helperSplit = token.Split(',');
            string[] otherHelperSplit = helperSplit[1].Split(':');
            string givenToken = otherHelperSplit[1].Replace("\"", "");
            return response;
        }

        private async Task<HttpResponseMessage> ErrorLoginEmptyInputs()
        {
            HttpClient client = new HttpClient();
            string url = "/api/users/login";
            client.BaseAddress = new Uri("http://127.0.0.1:8000");
            JObject jObject = new JObject();
            jObject.Add("userID", "");
            jObject.Add("password", "");
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);
            string token = response.Content.ReadAsStringAsync().Result;
            string[] helperSplit = token.Split(',');
            string[] otherHelperSplit = helperSplit[1].Split(':');
            string givenToken = otherHelperSplit[1].Replace("\"", "");
            return response;
        }

        private async Task<string> Token()
        {
            HttpResponseMessage response = await Login();
            string token = response.Content.ReadAsStringAsync().Result;
            string[] helperSplit = token.Split(',');
            string[] otherHelperSplit = helperSplit[1].Split(':');
            string givenToken = otherHelperSplit[1].Replace("\"", "");
            return givenToken;
        }





    }
}
