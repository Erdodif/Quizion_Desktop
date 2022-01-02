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

namespace Projekt
{
    /// <summary>
    /// Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : Window
    {
        static string Url = "http://quizion.hu/api/quizes";
        public DatabaseView()
        {
            
            InitializeComponent();
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
