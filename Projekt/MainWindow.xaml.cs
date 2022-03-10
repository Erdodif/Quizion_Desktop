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
        static Szinek szinek = new Szinek();
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
                string[] segedBonto = token.Split(',');
                string[] masikSegedBonto = segedBonto[1].Split(':');
                string atadoToken = masikSegedBonto[1].Replace("\"", "");
                DatabaseView adatbazisNezet = new DatabaseView();
                this.Visibility = Visibility.Hidden;
                this.Close();
                adatbazisNezet.Token = atadoToken;
                adatbazisNezet.Show();
            }
            else
            {
                string hiba = Convert.ToString(response.Content.ReadAsStringAsync().Result);
                tbl_message.Text = hiba.Replace(hiba, "Invalid userID or password!");
                tbl_message.Foreground = szinek.Warning;

            }
            
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            
            LoginAsync();
            System.Threading.Thread.Sleep(2000);
            btn_login.IsEnabled = false;
            btn_login.Background = szinek.OnPrimary;
            
        }

        
    }
}
