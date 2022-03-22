using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ColorsOfQuizion quizionColors = new ColorsOfQuizion();
        static HttpClient client = new HttpClient();
        string token;

        public string Token { get => token; set => token = value; }

        private async Task LoginAsync()
        {
            client = new HttpClient();
            string url = "/api/users/login";
           // client.BaseAddress = new Uri("http://quizion.hu");
            client.BaseAddress = new Uri("http://127.0.0.1:8000");
            JObject jObject = new JObject();
            jObject.Add("userID", tbx_name.Text);
            jObject.Add("password", tbx_pass.Password);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                token = response.Content.ReadAsStringAsync().Result;
                string[] helperSplit = token.Split(',');
                string[] otherHelperSplit = helperSplit[1].Split(':');
                string givenToken = otherHelperSplit[1].Replace("\"", "");
                DatabaseView databaseView = new DatabaseView();
                this.Visibility = Visibility.Hidden;
                this.Close();
                databaseView.Token = givenToken;
                databaseView.Show();
            }
            else
            {
                string error = Convert.ToString(response.Content.ReadAsStringAsync().Result);
                tbl_message.Text = error.Replace(error, "Invalid userID or password!");
                tbl_message.Foreground = quizionColors.Warning;
                btn_login.IsEnabled = true;
            }
            
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            LoginAsync();
            System.Threading.Thread.Sleep(2000);
            btn_login.IsEnabled = false;
            btn_login.Background = quizionColors.OnPrimary;
        }       
    }
}
