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
            client.BaseAddress = new Uri("http://quizion.hu");
            JObject jObject = new JObject();
            jObject.Add("userID", tbx_name.Text);
            jObject.Add("password", tbx_pass.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                tbl_hibak.Text = response.Content.ReadAsStringAsync().Result;
                token = response.Content.ReadAsStringAsync().Result;
                MessageBox.Show("Sikeres belépés!", "Üzenet", MessageBoxButton.OK, MessageBoxImage.Information);
                DatabaseView adatbazisNezet = new DatabaseView();
                this.Visibility = Visibility.Hidden;
                this.Close();
                adatbazisNezet.Token = token;
                adatbazisNezet.Show();
            }
            else
            {
                tbl_hibak.Text = response.Content.ReadAsStringAsync().Result;
                MessageBox.Show("Hiba", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {

            LoginAsync();
            btn_login.Background = szinek.OnPrimary;
            
            
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
