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
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Projekt
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            //Color primary = (Color)ColorConverter.ConvertFromString("#50508E");
            //Color primaryVariant = (Color)ColorConverter.ConvertFromString("#211A52");
            //Color secondary = (Color)ColorConverter.ConvertFromString("#7985C1");
            //Color secondaryVariant = (Color)ColorConverter.ConvertFromString("#5B6AB0");
            //Color onSecondary = (Color)ColorConverter.ConvertFromString("#4053A0");
            //Color onPrimary = (Color)ColorConverter.ConvertFromString("#E8E7F5");
            //Color warning = (Color)ColorConverter.ConvertFromString("#BA0100");
            //Color alert = (Color)ColorConverter.ConvertFromString("#BAA100");
            //Color fine = (Color)ColorConverter.ConvertFromString("#1CBA00");
            //Color black = (Color)ColorConverter.ConvertFromString("#FF000000");
            //Color white = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
                            MessageBox.Show("Sikertelen", "Üzenet", MessageBoxButton.OK, MessageBoxImage.Error);
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
