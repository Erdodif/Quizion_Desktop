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
            SolidColorBrush primary = new SolidColorBrush();
            primary = (SolidColorBrush)new BrushConverter().ConvertFrom("#50508E");


            SolidColorBrush primaryVariant = new SolidColorBrush();
            primaryVariant = (SolidColorBrush)new BrushConverter().ConvertFrom("#211A52");

            SolidColorBrush secondary = new SolidColorBrush();
            secondary = (SolidColorBrush)new BrushConverter().ConvertFrom("#7985C1");

            SolidColorBrush secondaryVariant = new SolidColorBrush();
            secondaryVariant = (SolidColorBrush)new BrushConverter().ConvertFrom("#5B6AB0");

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
               
                //lbl_listaz.Text = answer[0].ToString();
                //lista.Add(answer);
                //lbl_listaz.Text = lista[0];






            }


        }

      
    }
}
