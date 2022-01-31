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
      


        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://quizion.hu/api/user/login";
            string contente = "userId\npassword";
            StringContent content = new StringContent(contente);
            //MultipartContent multi = new MultipartContent();
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer");




            client.PostAsync(url, content);
            
             if (tbx_00.Text == "" || tbx_01.Text == "")
             {
                 tbl_hibak.Text = "Minden mező megadása kötelező!";
             }
             else if (tbx_00.Text.Length < 5)
             {
                 tbl_hibak.Text = "Túl rövid a név!";
             }
             else if (tbx_01.Text.Contains("@"))
             {
                 tbl_hibak.Text = "Érvénytelen jelszó!";
             }
             else if (tbx_00.Text.ToLower() == "admin")
             {
                 tbl_hibak.Text = "Nem megfelelő név!";
             }
             else if (tbx_01.Text.Length < 8)
             {
                 tbl_hibak.Text = "Túl rövid a jelszó!";
             }
             else if (tbx_01.Text.ToLower() == "password")
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
            

            
            


        }

       
    }
}
