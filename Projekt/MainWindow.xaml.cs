using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            else if (tbx_00.Text.ToLower() == "admin")
            {
                tbl_hibak.Text = "Nem megfelelő név!";
            }
            else if (tbx_01.Text.Length < 8)
            {
                tbl_hibak.Text = "Túl rövid a jelszó!";
            }
            else if (tbx_01.Text == "password")
            {
                tbl_hibak.Text = "Nem megfelelő jelszó!";
            }
            else
            {
                MessageBox.Show("Sikeres belépés!", "Üzenet", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            
           
        }
    }
}
