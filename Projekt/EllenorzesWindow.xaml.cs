using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.Http;
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
    /// Interaction logic for EllenorzesWindow.xaml
    /// </summary>
    public partial class EllenorzesWindow : Window
    {
        static string Url = "http://quizion.hu/api/quizes";
        static Szinek szinek = new Szinek();
        static List<string> lista = new List<string>();
        public EllenorzesWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {


            //keres();
            //Listazas("http://quizion.hu/api/quizes");

            btn_ellenorzo.Background = szinek.Alert;
            MessageBox.Show("Sikeres ellenőrzés", "Üzenet", MessageBoxButton.OK, MessageBoxImage.Information);
            DatabaseView adatbazisNezet = new DatabaseView();
            this.Visibility = Visibility.Hidden;
            this.Close();
            adatbazisNezet.Show();



        }

        private async Task Listazas(String url)

        {

            Data.Call(url);

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


                client.DefaultRequestHeaders.Add("charset", "utf8");
                string answer = await client.GetStringAsync(url);
                var adat = JsonConvert.DeserializeObject(answer);
                var formazva = JsonConvert.SerializeObject(adat, Formatting.Indented);
                lista.Add(formazva);
                lbl_listaz.Text = lista[0];

                







            }


        }



        private void keres()
        {
            // Create the http request




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
                        lbl_keres.Foreground = szinek.Primary;
                        lbl_listaz.Background = szinek.Secondary;
                        Kiiratas(results);
                        /*
                            if (webRequest.Method == "POST")
                            {
                                MessageBox.Show("Sikeres", "Üzenet", MessageBoxButton.OK, MessageBoxImage.Information);
                                DatabaseView adatbazisNezet = new DatabaseView();
                                this.Visibility = Visibility.Hidden;
                                this.Close();
                                adatbazisNezet.Show();
                            }
                            else
                            {
                                MessageBox.Show("Sikertelen", "Üzenet", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        */



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

