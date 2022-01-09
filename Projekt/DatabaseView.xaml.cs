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
        static string Url = "";
        public DatabaseView()
        {
            
            InitializeComponent();
            SolidColorBrush primary = new SolidColorBrush();
            primary = (SolidColorBrush)new BrushConverter().ConvertFrom("#50508E");


            SolidColorBrush primaryVariant = new SolidColorBrush();
            primaryVariant = (SolidColorBrush)new BrushConverter().ConvertFrom("#211A52");

            SolidColorBrush secondary = new SolidColorBrush();
            secondary = (SolidColorBrush)new BrushConverter().ConvertFrom("#7985C1");

            SolidColorBrush secondaryVariant = new SolidColorBrush();
            secondaryVariant = (SolidColorBrush)new BrushConverter().ConvertFrom("#5B6AB0");

            SolidColorBrush onSecondary = new SolidColorBrush();
            onSecondary = (SolidColorBrush)new BrushConverter().ConvertFrom("#4053A0");

            SolidColorBrush onPrimary = new SolidColorBrush();
            onPrimary = (SolidColorBrush)new BrushConverter().ConvertFrom("#E8E7F5");

            SolidColorBrush warning = new SolidColorBrush();
            warning = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA0100");

            SolidColorBrush alert = new SolidColorBrush();
            alert = (SolidColorBrush)new BrushConverter().ConvertFrom("#BAA100");

            SolidColorBrush fine = new SolidColorBrush();
            fine = (SolidColorBrush)new BrushConverter().ConvertFrom("#1CBA00");

            SolidColorBrush white = new SolidColorBrush();
            white = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF000000");

            SolidColorBrush black = new SolidColorBrush();
            black = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");

           
        }

        private async Task Kvizlistazas(String url)

        {


            using (var client = new HttpClient())
            {
                string valasz = await client.GetStringAsync(url);
                ApiAnswer<Quiz> data = JsonConvert.DeserializeObject<ApiAnswer<Quiz>>(valasz);
                foreach (var kviz in data.Adatok)
                {
                    lista.Items.Add(kviz);
                }





            }


        }


        private async Task Kerdeslistazas(String url)

        {


            using (var client = new HttpClient())
            {
                string valasz = await client.GetStringAsync(url);
                ApiAnswer<Question> data = JsonConvert.DeserializeObject<ApiAnswer<Question>>(valasz);
                foreach (var kerdes in data.Adatok)
                {
                    lista.Items.Add(kerdes);
                }





            }


        }

        private async Task Valaszlistazas(String url)

        {


            using (var client = new HttpClient())
            {
                string valasz = await client.GetStringAsync(url);
                ApiAnswer<Answer> data = JsonConvert.DeserializeObject<ApiAnswer<Answer>>(valasz);
                foreach (var valasza in data.Adatok)
                {
                    lista.Items.Add(valasza);
                }





            }


        }

        private void QuizClick(object sender, RoutedEventArgs e)
        {
            Kvizlistazas("http://quizion.hu/api/quizes").GetAwaiter().GetResult();
        }

        private void QuestionClick(object sender, RoutedEventArgs e)
        {
            Kerdeslistazas("http://quizion.hu/api/questions").GetAwaiter().GetResult();
        }

        private void AnswerClick(object sender, RoutedEventArgs e)
        {
            Valaszlistazas("http://quizion.hu/api/answers").GetAwaiter().GetResult();
        }

        private void AdminClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
