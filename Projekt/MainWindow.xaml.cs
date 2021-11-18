using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
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
        
     
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            
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
                MessageBox.Show("Sikeres belépés!", "Üzenet", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            keres();
           
        }

        private void keres()
        {
            // Create the http request
           
            const string Url = "http://10.147.20.1/adatok/index.php?method=read&table=quiz";
            var webRequest = WebRequest.Create(Url);

            // Send the http request and wait for the response
            var responseStream = webRequest.GetResponse().GetResponseStream();
            webRequest.Method = "POST";
            
            // Displays the response stream text
            if (responseStream != null)
            {
                using (var streamReader = new StreamReader(responseStream))
                {
                    // Return next available character or -1 if there are no characters to be read
                    lbl_keres.Text = streamReader.Peek().ToString() + " ";
                    while (streamReader.Peek() > -1)
                    {
                        string visszaad = streamReader.ReadLine();
                        //lbl_keres.Text += visszaad;
                        JsonSerializer.Create();
                        JObject tartalom = JObject.Parse(visszaad);
                        IList<JToken> results = tartalom["data"].Children().ToList();
                        string osszegyujt = "";
                        /*List<Quiz> kvizek = new List<Quiz>();
                        foreach (JToken result in visszaad)
                        {
                            kvizek.Add(result.ToObject<Quiz>());
                        }
                        foreach (Quiz kviz in kvizek)
                        {
                            osszegyujt += kviz + "\n";
                        }
                        */
                        Olvasas(results);
                        //lbl_keres.Text = tartalom["data"][1]["header"].ToString();
                        //lbl_keres.Text += tartalom["data"][1]["description"].ToString();
                        Kiiratas(results);
                        if (webRequest.Method == "POST")
                        {
                            MessageBox.Show("Sikeres", "Üzenet", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Sikeres", "Üzenet", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                }
            }


            
        }

        private void Olvasas(IList<JToken> jtoken)
        {
            
            lbl_keres.Text = jtoken[0].ToString();
         
        }


        private void Kiiratas(IList<JToken> jtoken)
        {
            for (int i = 0; i < jtoken.Count; i++)
            {
                lbl_listaz.Text += jtoken[i];
            }
        }
    }
}
