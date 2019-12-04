using AccesoDatos;
using Entidades;
using ParteVisual.vistas;
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

namespace ParteVisual
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VTipoCargo vTipoCargo = new VTipoCargo();
            vTipoCargo.ShowDialog();
        }

        private void btnReciboSueldo_Click(object sender, RoutedEventArgs e)
        {
            VReciboSueldo vReciboSueldo = new VReciboSueldo();
            vReciboSueldo.ShowDialog();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VTipoConcepto vTipoConcepto = new VTipoConcepto();
            vTipoConcepto.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            VCargo vCargo = new VCargo();
            this.Hide();
            vCargo.ShowDialog();
            this.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            VPersona vPersona = new VPersona();
            this.Hide();
            vPersona.ShowDialog();
            this.Show();
        }
    }
}
