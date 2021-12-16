using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Http;
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
        static string Url = "http://quizion.hu/api/quizes";
        static List<string> lista = new List<string>();
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
            //keres();
            listazas("http://quizion.hu/api/quizes");
            

        }

        private async Task listazas(String url)

        {
            using (var client = new HttpClient())
            {
                /*
                Encoding enc = Encoding.ASCII;
                string json = JsonConvert.SerializeObject(url);
                var adat = new StringContent(json, Encoding.UTF8, "application/json");
                //lbl_listaz.Text = json;
                lbl_listaz.Text = url;

                */
                //var adata = new StringContent(answer, Encoding.ASCII, "application/json");


                // működik de nem magyar kódolás
                string answer = await client.GetStringAsync(url);
                lbl_listaz.Text = answer[0].ToString();
                lista.Add(answer);
                lbl_listaz.Text = lista[0];






            }


        }



        private void keres()
        {
            // Create the http request

            SolidColorBrush primary = new SolidColorBrush();
            primary = (SolidColorBrush) new BrushConverter().ConvertFrom("#50508E");


            SolidColorBrush primaryVariant = new SolidColorBrush();
            primaryVariant = (SolidColorBrush) new BrushConverter().ConvertFrom("#211A52");

            SolidColorBrush secondary = new SolidColorBrush();
            secondary = (SolidColorBrush) new BrushConverter().ConvertFrom("#7985C1");

            SolidColorBrush secondaryVariant = new SolidColorBrush();
            secondaryVariant = (SolidColorBrush) new BrushConverter().ConvertFrom("#5B6AB0");

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

            //const string Url = "http://10.147.20.1/adatok/index.php?method=read&table=quiz";
            const string Url = "http://quizion.hu/api/quizes";

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
                        IList<JToken> results = tartalom.Children().ToList();
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
                        lbl_keres.Foreground = primary;
                        lbl_listaz.Background = secondaryVariant;
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
        /*
        public SolidColorBrush SzinekHexaKodbol(string hexaSzinKod)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hexaSzinKod));
        }
        */

    }
}
