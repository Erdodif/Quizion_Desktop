using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;



namespace Projekt
{
    /// <summary>
    /// Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : Window
    {
        static string url = "";
        static HttpClient client = new HttpClient();
        static List<string> listam = new List<string>();
        static Szinek szinek = new Szinek();
        public DatabaseView()
        {
            
            InitializeComponent();
           
           
        }

        private async Task Kvizlistazas(string url)

        {
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string valasz = await client.GetStringAsync(url);
            valasz.Trim();
            lista.Text = valasz[0].ToString();
            listam.Add(valasz);
            lista.Text = listam[0];
            //List<Quiz> quiz = JsonConvert.DeserializeObject<List<Quiz>>(valasz);
            //ApiAnswer<Quiz> data = JsonConvert.DeserializeObject<ApiAnswer<Quiz>>(valasz);
            /*foreach (Quiz item in quiz)
            {
                lista.Text = $"{item.Id}\t${item.Header}\t{item.Active}\t{item.Description}\t{item.SecondsPerQuiz}\n";
            }

            /*client.DefaultRequestHeaders.Add("charset", "utf8");
            string answer = await client.GetStringAsync(url);
            listaz.Text = answer[0].ToString();
            lista.Add(answer);
            listaz.Text = lista[0];
            */


            


        }


        private async Task Kerdeslistazas(string url)

        {

            listam.Clear();
            string valasz = await client.GetStringAsync(url);
            valasz.Trim();
            lista.Text = valasz[0].ToString();
            listam.Add(valasz);
            lista.Text = listam[0];

            /*string valasz = await client.GetStringAsync(url);
            ApiAnswer<Question> data = JsonConvert.DeserializeObject<ApiAnswer<Question>>(valasz);
            foreach (var kerdes in data.Adatok)
            {
                lista.Items.Add(kerdes);
            }
            */








        }

        private async Task Valaszlistazas(string url)

        {


            listam.Clear();
            string valasz = await client.GetStringAsync(url);
            valasz.Trim();
            lista.Text = valasz[0].ToString();
            listam.Add(valasz);
            lista.Text = listam[0];
            /* string valasz = await client.GetStringAsync(url);
            ApiAnswer<Answer> data = JsonConvert.DeserializeObject<ApiAnswer<Answer>>(valasz);
            foreach (var valasza in data.Adatok)
            {
                lista.Items.Add(valasza);
            }
            */








        }

        private void QuizClick(object sender, RoutedEventArgs e)
        {
            Kvizlistazas("http://quizion.hu/api/quizes");
        }

        private void QuestionClick(object sender, RoutedEventArgs e)
        {
            Kerdeslistazas("http://quizion.hu/api/questions");
        }

        private void AnswerClick(object sender, RoutedEventArgs e)
        {
            Valaszlistazas("http://quizion.hu/api/answers");
        }

        private void AdminClick(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
