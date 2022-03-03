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
        static Szinek szinek = new Szinek();

        string token;

        public string Token { get => token; set => token = value; }
        public DatabaseView()
        {
            
            InitializeComponent();
            btn_adminjog.Visibility = Visibility.Hidden;

        }

        private async Task Kvizlistazas(string url)

        {

            lista.Columns.Clear();
            string valasz = await client.GetStringAsync(url);
            List<Quiz> quiz = JsonConvert.DeserializeObject<List<Quiz>>(valasz);
            lista.ItemsSource = quiz;
            foreach (var item in quiz)
            {
                lista.Items.Add(item);
            }



        }


        private async Task Kerdeslistazas(string url)

        {
            lista.Columns.Clear();
            string valasz = await client.GetStringAsync(url);
            List<Question> question = JsonConvert.DeserializeObject<List<Question>>(valasz);
            lista.ItemsSource = question;
            foreach (var item in question)
            {
                lista.Items.Add(item);
            }



        }

        private async Task Valaszlistazas(string url)

        {
            lista.Columns.Clear();
            string valasz = await client.GetStringAsync(url);
            List<Answer> answer = JsonConvert.DeserializeObject<List<Answer>>(valasz);
            lista.ItemsSource = answer;
            foreach (var item in answer)
            {
                lista.Items.Add(item);
            }


        }

        private async Task UserListazas(string url)
        {
            lista.Columns.Clear();
            string valasz = await client.GetStringAsync(url);
            List<User> user = JsonConvert.DeserializeObject<List<User>>(valasz);
            lista.ItemsSource = user;
            foreach (var item in user)
            {
                lista.Items.Add(item);
            }
            btn_hozzaado.IsEnabled = false;
            
        }

        private void QuizClick(object sender, RoutedEventArgs e)
        {
            //Kvizlistazas("http://quizion.hu/api/quizzes/all");
            Kvizlistazas("http://127.0.0.1:8000/api/quizzes/all");
            btn_hozzaado.IsEnabled = true;
            btn_adminjog.Visibility = Visibility.Hidden;
        }

        private void QuestionClick(object sender, RoutedEventArgs e)
        {
            //Kerdeslistazas("http://quizion.hu/api/questions");
            Kerdeslistazas("http://127.0.0.1:8000/api/questions");
            btn_hozzaado.IsEnabled = true;
            btn_adminjog.Visibility = Visibility.Hidden;

        }

        private void AnswerClick(object sender, RoutedEventArgs e)
        {
            //Valaszlistazas("http://quizion.hu/api/answers");
            Valaszlistazas("http://127.0.0.1:8000/api/answers");
            btn_hozzaado.IsEnabled = true;
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
            string kijelolt = lista.SelectedItem.ToString();
            string[] st = kijelolt.Split(';');
            int index = Int32.Parse(st[0]);
            //int index = lista.Items.IndexOf(lista.SelectedItem);
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
                        tbx_00.IsEnabled = false;
                        

                        if (tbx_01.Text.Length < 3)
                        {
                            tbl_status.Text = "Nem megfelelő karakter hosszúságú a Quiz headerje!";
                        }
                        else if (tbx_02.Text.Length < 4)
                        {
                            tbl_status.Text = "Nem megfelelő karakter hosszúságú a Quiz descriptionje!";
                        }
                        else
                        {
                            ModositasQuiz(index);
                            Kvizlistazas("http://127.0.0.1:8000/api/quizzes/all");
                            //Kvizlistazas("http://quizion.hu/api/quizzes/all");
                        }
                        

                    }
                    else if (lista.SelectedItem is Question)
                    {
                        tbx_00.IsEnabled = true;
                        if (tbx_01.Text.Length < 3)
                        {
                            tbl_status.Text = "Nem megfelelő karakter hosszúságú a Question contente!";
                        }
                        else
                        {
                            ModositasQuestion(index);
                            Kerdeslistazas("http://127.0.0.1:8000/api/questions");
                            //Kerdeslistazas("http://quizion.hu/api/questions");
                        }
                        
                    }
                    else if (lista.SelectedItem is Answer)
                    {
                        tbx_00.IsEnabled = true;
                        if (tbx_01.Text.Length < 3)
                        {
                            tbl_status.Text = "Nem megfelelő karakter hosszúságú az Answer contente!";
                        }
                       
                        else
                        {
                            ModositasAnswer(index);
                            Valaszlistazas("http://127.0.0.1:8000/api/answers");
                            //Valaszlistazas("http://quizion.hu/api/answers");
                        }
                        
                    }

                    else if (lista.SelectedItem is User)
                    {
                        tbx_00.IsEnabled = true;
                        if (tbx_01.Text.Length < 3)
                        {
                            tbl_status.Text = "Nem megfelelő karakter hosszúságú a User neve!";
                        }

                        else
                        {
                            ModositasUser(index);
                            UserListazas("http://127.0.0.1:8000/api/users");
                            //UserListazas("http://quizion.hu/api/users");
                        }

                    }
                    else
                    {
                        tbl_status.Text = "Hiba, nem sikerült a módosítást végrehajtani!";
                    }



                }
                else
                {
                    //Nem történik semmi, ha nem szeretnénk módosítani!
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
            jObject.Add("quiz_id", tbx_00.Text);
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
            jObject.Add("question_id", tbx_00.Text);
            jObject.Add("content", tbx_01.Text);
            jObject.Add("is_right", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/answers/{id}", stringContent);
            tbl_status.Text = response.ToString();

        }

        private async Task ModositasUser(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //string url = "http://quizion.hu/api/users";
            JObject jObject = new JObject();
            jObject.Add("user_id", tbx_00.Text);
            jObject.Add("name", tbx_01.Text);
            jObject.Add("xp", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/users/{id}", stringContent);
            tbl_status.Text = response.ToString();

        }

        private void Torles(object sender, RoutedEventArgs e)
        {
            string kijelolt = lista.SelectedItem.ToString();
            string[] st = kijelolt.Split(';');

            int index = Convert.ToInt32(st[0]);
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
                        tbx_01.Text = "";
                        tbx_02.Text = "";
                        Kvizlistazas("http://127.0.0.1:8000/api/quizzes/all");
                        //Kvizlistazas("http://quizion.hu/api/quizzes/all");


                    }
                    else if (lista.SelectedItem is Question)
                    {
                        KerdesTorlese(index);
                        tbx_00.Text = "";
                        tbx_01.Text = "";
                        tbx_02.Text = "";
                        Kerdeslistazas("http://127.0.0.1:8000/api/questions");
                        //Kerdeslistazas("http://quizion.hu/api/questions");
                    }
                    else if (lista.SelectedItem is Answer)
                    {
                        ValaszTorlese(index);
                        tbx_00.Text = "";
                        tbx_01.Text = "";
                        tbx_02.Text = "";
                        Valaszlistazas("http://127.0.0.1:8000/api/answers");
                        //Valaszlistazas("http://quizion.hu/api/answers");
                    }
                    else if (lista.SelectedItem is User)
                    {
                        UserTorlese(index);
                        tbx_00.Text = "";
                        tbx_01.Text = "";
                        tbx_02.Text = "";
                        UserListazas("http://127.0.0.1:8000/api/users");
                        //UserListazas("http://quizion.hu/api/users");

                    }
                    else
                    {
                        tbl_status.Text = "Hiba, nem sikerült a törlést végrehajtani!";
                    }

                    MessageBox.Show("A kiválasztott elem sikeresen törölve", "Sikeres törlés a listából", MessageBoxButton.OK, MessageBoxImage.Question);
                    //lista.Items.RemoveAt(lista.Items.IndexOf(lista.SelectedItem));

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

        private async Task UserTorlese(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //client.BaseAddress = new Uri("http://quizion.hu/");
            var response = await client.DeleteAsync($"api/users/{id}");
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
            jObject.Add("quiz_id", tbx_00.Text);
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
            jObject.Add("question_id", tbx_00.Text);
            jObject.Add("content", tbx_01.Text);
            jObject.Add("is_right", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            tbl_status.Text = response.ToString();
        }
        private void HozzaadasClick(object sender, RoutedEventArgs e)
        {
            if (cbx.SelectedItem == cbx_quiz)
            {
                if (tbx_01.Text.Length < 3)
                {
                    tbl_status.Text = "Nem megfelelő karakter hosszúságú a Quiz headerje!";
                }
                else if (tbx_02.Text.Length < 4)
                {
                    tbl_status.Text = "Nem megfelelő karakter hosszúságú a Quiz descriptionje!";
                }
                else
                {
                    KvizHozzaadasa();
                    Kvizlistazas("http://127.0.0.1:8000/api/quizzes/all");
                    //Kvizlistazas("http://quizion.hu/api/quizzes/all");
                }
                
            }

            else if (cbx.SelectedItem == cbx_question)
            {
                if (tbx_01.Text.Length < 3)
                {
                    tbl_status.Text = "Nem megfelelő karakter hosszúságú a Question contente!";
                }
                else
                {
                    KerdesHozzaadasa();
                    Kerdeslistazas("http://127.0.0.1:8000/api/questions");
                    //Kerdeslistazas("http://quizion.hu/api/questions");
                }
                
            }

            else if (cbx.SelectedItem == cbx_answer)
            {
                if (tbx_01.Text.Length < 3)
                {
                    tbl_status.Text = "Nem megfelelő karakter hosszúságú az Answer contente!";
                }
                else
                {
                    ValaszHozzaadasa();
                    Valaszlistazas("http://127.0.0.1:8000/api/answers");
                    //Valaszlistazas("http://quizion.hu/api/answers");
                }
                
            }

            else
            {
                tbl_status.Text = "Hiba, nem sikerült a hozzáadást végrehajtani!";
            }

            
            


        }

        private void cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx.SelectedItem == cbx_quiz)
            {
                tbx_00.IsEnabled = false;
              
                
            }

            else if (cbx.SelectedItem == cbx_question)
            {
                
                tbx_00.IsEnabled = true;
               
                
            }

            else if (cbx.SelectedItem == cbx_answer)
            {
               
                tbx_00.IsEnabled = true;
               
            }
        }

        private void lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lista.SelectedIndex != -1)
            {
                if (lista.SelectedItem is Quiz)
                {
                    tbx_00.IsEnabled = false;
                    string kijelolt = lista.SelectedItem.ToString();
                    string[] st = kijelolt.Split(';');
                    tbx_01.Text = st[1];
                    tbx_02.Text = st[2];
                }

                else if (lista.SelectedItem is Question)
                {
                    tbx_00.IsEnabled = true;
                    string kijelolt = lista.SelectedItem.ToString();
                    string[] st = kijelolt.Split(';');
                    tbx_00.Text = st[1];
                    tbx_01.Text = st[2];
                    tbx_02.Text = st[3];
                }

                else if (lista.SelectedItem is Answer)
                {
                    tbx_00.IsEnabled = true;
                    string kijelolt = lista.SelectedItem.ToString();
                    string[] st = kijelolt.Split(';');
                    tbx_00.Text = st[1];
                    tbx_01.Text = st[2];
                    tbx_02.Text = st[3];
                }

                else if (lista.SelectedItem is User)
                {
                    tbx_00.IsEnabled = true;
                    string kijelolt = lista.SelectedItem.ToString();
                    string[] st = kijelolt.Split(';');
                    tbx_00.Text = st[0];
                    tbx_01.Text = st[1];
                    tbx_02.Text = st[2];
                }
            }

                

            }
        }
    }

    // /api/quiz/1/question/1/answer/1
    // /api/quiz/1/question/1/answers
    // /api/quiz/1/questions

