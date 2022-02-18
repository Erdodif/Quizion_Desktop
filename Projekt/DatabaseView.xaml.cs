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
        static List<Quiz> quiz;

        string token;

        public string Token { get => token; set => token = value; }
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
            quiz = JsonConvert.DeserializeObject<List<Quiz>>(valasz);
            foreach (var item in quiz)
            {
                lista.Items.Add(item);
            }


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



        }

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
            //Kvizlistazas("http://quizion.hu/api/quizzes");
            Kvizlistazas("http://127.0.0.1:8000/api/quizzes");
            btn_adminjog.Visibility = Visibility.Hidden;
        }

        private void QuestionClick(object sender, RoutedEventArgs e)
        {
            //Kerdeslistazas("http://quizion.hu/api/questions");
            Kerdeslistazas("http://127.0.0.1:8000/api/questions");
            btn_adminjog.Visibility = Visibility.Hidden;

        }

        private void AnswerClick(object sender, RoutedEventArgs e)
        {
            //Valaszlistazas("http://quizion.hu/api/answers");
            Valaszlistazas("http://127.0.0.1:8000/api/answers");
            btn_adminjog.Visibility = Visibility.Hidden;
        }

        private void AdminClick(object sender, RoutedEventArgs e)
        {
            btn_adminjog.Visibility = Visibility.Visible;
        }

        private void UserClick(object sender, RoutedEventArgs e)
        {
            //UserListazas("http://quizion.hu/api/users");
            UserListazas("http://127.0.0.1:8000/api/users");
            btn_adminjog.Visibility = Visibility.Visible;
        }

        private void ModositasClick(object sender, RoutedEventArgs e)
        {

            int index = lista.Items.IndexOf(lista.SelectedItem);
            if (lista.SelectedIndex == -1)
            {
                MessageBox.Show("Nincsen kiválasztva elem a listából a módosítás előtt", "Érvénytelen módosítás", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Biztos vagy benne, hogy módosítani szeretnéd az alábbi elemet: {lista.SelectedItem} ", "Figyelmeztetés", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {

                    if (lista.SelectedItem is Quiz)
                    {
                        ModositasQuiz(index);
                        Kvizlistazas("http://127.0.0.1:8000/api/quizzes");

                    }
                    else if (lista.SelectedItem is Question)
                    {
                        ModositasQuestion(index);
                        Kerdeslistazas("http://127.0.0.1:8000/api/questions");
                    }
                    else if (lista.SelectedItem is Answer)
                    {
                        ModositasAnswer(index);
                        Valaszlistazas("http://127.0.0.1:8000/api/answers");
                    }
                    else
                    {
                        tbl_status.Text = "Hiba, nem sikerült a módosítást végrehajtani!";
                    }



                }
                else
                {
                    //Nem történik semmi, ha nem szeretnénk törölni!
                }
            }
        }

        private async Task ModositasQuiz(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //string url = "http://quizion.hu/api/quizzes";
            JObject jObject = new JObject();
            jObject.Add("header", tbx_01.Text);
            jObject.Add("description", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/quizzes/{id}", stringContent);
            tbl_status.Text = response.ToString();

        }

        private async Task ModositasQuestion(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //string url = "http://quizion.hu/api/questions";
            JObject jObject = new JObject();
            jObject.Add("content", tbx_01.Text);
            jObject.Add("point", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/questions/{id}", stringContent);
            tbl_status.Text = response.ToString();

        }

        private async Task ModositasAnswer(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //string url = "http://quizion.hu/api/answers";
            JObject jObject = new JObject();
            jObject.Add("content", tbx_01.Text);
            jObject.Add("is_right", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/answers/{id}", stringContent);
            tbl_status.Text = response.ToString();

        }

        private void Torles(object sender, RoutedEventArgs e)
        {
            int index = lista.Items.IndexOf(lista.SelectedItem);
            if (lista.SelectedIndex == -1)
            {
                MessageBox.Show("Nincsen kiválasztva elem a listából a törlés előtt", "Érvénytelen törlés", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Biztos vagy benne, hogy törölni szeretnéd az alábbi elemet: {lista.SelectedItem} ", "Figyelmeztetés", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {

                    if (lista.SelectedItem is Quiz)
                    {
                        KvizTorlese(index);
                        Kvizlistazas("http://127.0.0.1:8000/api/quizzes");


                    }
                    else if (lista.SelectedItem is Question)
                    {
                        KerdesTorlese(index);
                        Kerdeslistazas("http://127.0.0.1:8000/api/questions");
                    }
                    else if (lista.SelectedItem is Answer)
                    {
                        ValaszTorlese(index);
                        Valaszlistazas("http://127.0.0.1:8000/api/answers");
                    }
                    else
                    {
                        tbl_status.Text = "Hiba, nem sikerült a törlést végrehajtani!";
                    }

                    MessageBox.Show("A kiválasztott elem sikeresen törölve", "Sikeres törlés a listából", MessageBoxButton.OK, MessageBoxImage.Question);
                    lista.Items.RemoveAt(lista.Items.IndexOf(lista.SelectedItem));

                }
                else
                {
                    //Nem történik semmi, ha nem szeretnénk törölni!
                }

            }

        }



        private async Task KvizTorlese(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //client.BaseAddress = new Uri("http://quizion.hu/");
            var response = await client.DeleteAsync($"api/quizzes/{id}");
            tbl_status.Text = response.ToString();

        }

        private async Task KerdesTorlese(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //client.BaseAddress = new Uri("http://quizion.hu/");
            var response = await client.DeleteAsync($"api/questions/{id}");
            tbl_status.Text = response.ToString();

        }

        private async Task ValaszTorlese(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //client.BaseAddress = new Uri("http://quizion.hu/");
            var response = await client.DeleteAsync($"api/answers/{id}");
            tbl_status.Text = response.ToString();

        }
        private void ToAdmin(object sender, RoutedEventArgs e)
        {

        }

        private async Task KvizHozzaadasa()
        {
            client = new HttpClient();
            string url = "/api/quizzes/";
            // client.BaseAddress = new Uri("http://quizion.hu");
            client.BaseAddress = new Uri("http://127.0.0.1:8000");
            JObject jObject = new JObject();
            jObject.Add("header", tbx_01.Text);
            jObject.Add("description", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            tbl_status.Text = response.ToString();
        }

        private async Task KerdesHozzaadasa()
        {
            client = new HttpClient();
            string url = "/api/questions/";
            // client.BaseAddress = new Uri("http://quizion.hu");
            client.BaseAddress = new Uri("http://127.0.0.1:8000");
            JObject jObject = new JObject();
            jObject.Add("content", tbx_01.Text);
            jObject.Add("point", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            tbl_status.Text = response.ToString();
        }

        private async Task ValaszHozzaadasa()
        {
            client = new HttpClient();
            string url = "/api/answers/";
            // client.BaseAddress = new Uri("http://quizion.hu");
            client.BaseAddress = new Uri("http://127.0.0.1:8000");
            JObject jObject = new JObject();
            jObject.Add("content", tbx_01.Text);
            jObject.Add("is_right", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            tbl_status.Text = response.ToString();
        }
        private void HozzaadasClick(object sender, RoutedEventArgs e)
        {
            KvizHozzaadasa();
            
           


           /* KerdesHozzaadasa();
            Kerdeslistazas("http://127.0.0.1:8000/api/questions");

            ValaszHozzaadasa();
            Valaszlistazas("http://127.0.0.1:8000/api/answers");
            

            tbl_status.Text = "Hiba, nem sikerült a hozzáadást végrehajtani!";
            */


        }
    }

    // /api/quiz/1/question/1/answer/1
    // /api/quiz/1/question/1/answers
    // /api/quiz/1/questions
}
