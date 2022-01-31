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
      

        private async Task LoginAsync()
        {
            string url = "http://quizion.hu/api/user/login";
            string j = $"{tbx_name.Text}\n{tbx_pass.Text}";
            string[] sztr = { "userId", "password" };
            string content = JsonConvert.SerializeObject(sztr);
            StringContent stringContent = new StringContent(content);
            stringContent.Headers.Add("userId", tbx_name.Text);
            stringContent.Headers.Add("password", tbx_pass.Text);
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);
            tbl_hibak.Text = response.Content.ReadAsStringAsync().Result;
            

        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {

            LoginAsync();
            /*
             if (tbx_name.Text == "" || tbx_pass.Text == "")
             {
                 tbl_hibak.Text = "Minden mező megadása kötelező!";
             }
             else if (tbx_name.Text.Length < 5)
             {
                 tbl_hibak.Text = "Túl rövid a név!";
             }
             else if (tbx_pass.Text.Contains("@"))
             {
                 tbl_hibak.Text = "Érvénytelen jelszó!";
             }
             else if (tbx_name.Text.ToLower() == "admin")
             {
                 tbl_hibak.Text = "Nem megfelelő név!";
             }
             else if (tbx_name.Text.Length < 8)
             {
                 tbl_hibak.Text = "Túl rövid a jelszó!";
             }
             else if (tbx_pass.Text.ToLower() == "password")
             {
                 tbl_hibak.Text = "Nem megfelelő jelszó!";
             }
             else
             {

                tbl_hibak.Text = "";
                MessageBox.Show("Sikeres belépés!", "Üzenet", MessageBoxButton.OK, MessageBoxImage.Information);
                EllenorzesWindow ellenorzoAblak = new EllenorzesWindow();
                this.Visibility = Visibility.Hidden;
                this.Close();
                ellenorzoAblak.Show();


            }
            */
            

            
            


        }

       
    }
}
