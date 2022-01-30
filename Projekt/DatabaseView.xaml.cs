using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace Projekt
{
    /// <summary>
    /// Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : Window
    {
        static string url = "";
        static HttpClient client = new HttpClient();
        //static List<string> listam = new List<string>();
        static Szinek szinek = new Szinek();
        public DatabaseView()
        {

            InitializeComponent();
            btn_adminjog.Visibility = Visibility.Hidden;

        }

        private async Task Kvizlistazas(string url)

        {

            lista.Items.Clear();
            lista.Items.Add($"{"id ",-4}{"header ",-10}{"description ",35} {"active ",8} {"secondsPerQuiz ",6}");
            string valasz = await client.GetStringAsync(url);
            List<Quiz> quiz = JsonConvert.DeserializeObject<List<Quiz>>(valasz);
            foreach (var item in quiz)
            {
                lista.Items.Add(item);
            }

            /* if (lista.SelectedIndex != -1)
             {
                 KivalasztottKvizKerdesei("http://quizion.hu/api/quiz/?lista.SelectedIndex");
             }
            */


            //FORMÁZOTT JSON

            /*
            string valasz = await client.GetStringAsync(url);
            var adat = JsonConvert.DeserializeObject(valasz);
            var formazott = JsonConvert.SerializeObject(adat, Formatting.Indented);
            listam.Add(formazott);
            lista.Items.Add(listam[0]);
            */

            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //string valasz = await client.GetStringAsync(url);
            //valasz.Trim();
            //lista.Text = valasz[0].ToString();
            //listam.Add(valasz);
            //lista.Text = listam[0];
            //List<Quiz> quiz = JsonConvert.DeserializeObject<List<Quiz>>(valasz);

            /*client.DefaultRequestHeaders.Add("charset", "utf8");
            string answer = await client.GetStringAsync(url);
            listaz.Text = answer[0].ToString();
            lista.Add(answer);
            listaz.Text = lista[0];
            */
            

        }


        private async Task Kerdeslistazas(string url)

        {
            lista.Items.Clear();
            lista.Items.Add($"{"id ",4} {"quizId ",4}  {"content ",25} {"point",5}");
            string valasz = await client.GetStringAsync(url);
            List<Question> question = JsonConvert.DeserializeObject<List<Question>>(valasz);
            foreach (var item in question)
            {
                lista.Items.Add(item);
            }

            //FORMÁZOTT JSON
            /*
            string valasz = await client.GetStringAsync(url);
            var adat = JsonConvert.DeserializeObject(valasz);
            var formazott = JsonConvert.SerializeObject(adat, Formatting.Indented);
            listam.Add(formazott);
            lista.Items.Add(listam[0]);
            */



            //valasz.Trim()
            //lista.Items.Add(valasz[0].ToString());
            //listam.Add(valasz);
            //lista.Items.Add(listam[0]);
            


        }
/*
        private async Task KivalasztottKvizKerdesei(string url)
        {
            string valasz = await client.GetStringAsync(url);
            List<Question> answer = JsonConvert.DeserializeObject<List<Question>>(valasz);
            foreach (var item in answer)
            {
                kivalasztLista.Items.Add(item);
            }
        }
*/
        private async Task Valaszlistazas(string url)

        {
            lista.Items.Clear();
            lista.Items.Add($"{"id ",3} {"questionId ",4}  {"content ",25} {"isRight ",4}");
            string valasz = await client.GetStringAsync(url);
            List<Answer> answer = JsonConvert.DeserializeObject<List<Answer>>(valasz);
            foreach (var item in answer)
            {
                lista.Items.Add(item);
            }

            //Formázott json
            /*string valasz = await client.GetStringAsync(url);
            var adat = JsonConvert.DeserializeObject(valasz);
            var formazott = JsonConvert.SerializeObject(adat, Formatting.Indented);
            listam.Add(formazott);
            lista.Items.Add(listam[0]);
            */




            //listam.Clear();
            //string valasz = await client.GetStringAsync(url);
            //valasz.Trim();
            //lista.Text = valasz[0].ToString();
            //listam.Add(valasz);
            //lista.Text = listam[0];
            

        }

        private async Task UserListazas(string url)
        {
            lista.Items.Clear();
            lista.Items.Add("name : email");
            string valasz = await client.GetStringAsync(url);
            List<User> user = JsonConvert.DeserializeObject<List<User>>(valasz);
            foreach (var item in user)
            {
                lista.Items.Add(item);
            }
        }

        private void QuizClick(object sender, RoutedEventArgs e)
        {
            Kvizlistazas("http://quizion.hu/api/quizzes");
            //Kvizlistazas("http://127.0.0.1:8000/api/quizzes");
            btn_adminjog.Visibility = Visibility.Hidden;
        }

        private void QuestionClick(object sender, RoutedEventArgs e)
        {
            Kerdeslistazas("http://quizion.hu/api/questions");
            //Kerdeslistazas("http://127.0.0.1:8000/api/questions");
            btn_adminjog.Visibility = Visibility.Hidden;

        }

        private void AnswerClick(object sender, RoutedEventArgs e)
        {
            Valaszlistazas("http://quizion.hu/api/answers");
            //Valaszlistazas("http://127.0.0.1:8000/api/answers");
            btn_adminjog.Visibility = Visibility.Hidden;
        }

        private void AdminClick(object sender, RoutedEventArgs e)
        {
            btn_adminjog.Visibility = Visibility.Visible;
        }

        private void UserClick(object sender, RoutedEventArgs e)
        {
            UserListazas("http://quizion.hu/api/users");
            btn_adminjog.Visibility = Visibility.Visible;
        }

        private void ModositasClick(object sender, RoutedEventArgs e)
        {

        }

        private void Torles(object sender, RoutedEventArgs e)
        {
            int item = lista.Items.IndexOf(lista.SelectedItem);
            if (lista.SelectedIndex == -1)
            {
                MessageBox.Show("Nincsen kiválasztva elem a listából a törlés előtt", "Érvénytelen törlés", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
           
                TorlesHivas(item);

                //TorlesHivas($"http://127.0.0.1:8000/api/quiz/{item}");
                



                MessageBox.Show("A kiválasztott elem sikeresen törölve a listából (nem az adatbázisból)", "Sikeres törlés a listából", MessageBoxButton.OK, MessageBoxImage.Question);
                lista.Items.RemoveAt(lista.Items.IndexOf(lista.SelectedItem));
            }

        }

        private async Task TorlesHivas(int id)
        {
            string url = "http://quizion.hu/api/quiz/";
            int i = lista.Items.IndexOf(lista.SelectedItem);
            using (HttpResponseMessage response = await client.DeleteAsync($"{url}/{i}"))
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();
            }
           
            
            
        }
        private void ToAdmin(object sender, RoutedEventArgs e)
        {

        }

        
    }

    // /api/quiz/1/question/1/answer/1
    // /api/quiz/1/question/1/answers
    // /api/quiz/1/questions
}
