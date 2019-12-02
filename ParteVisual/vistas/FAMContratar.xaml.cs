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
using System.Windows.Shapes;
using AccesoDatos;
using Entidades;

namespace ParteVisual.vistas
{
    /// <summary>
    /// Lógica de interacción para FAMContratar.xaml
    /// </summary>
    public partial class FAMContratar : Window
    {
        BDPersona bdPersona;
        List<Persona> SelectPersonas;
        TipoCargo tipoCargo;
        BDCargo bdCargo;

        public FAMContratar()
        {
            InitializeComponent();
            bdPersona = new BDPersona();
            bdCargo = new BDCargo();
            SelectPersonas = new List<Persona>();
            SelectPersonas = bdPersona.SelectPersonas();
            //MessageBox.Show(SelectPersonas[0].ToString());
            cbxPersonas.ItemsSource = SelectPersonas;

        }
        public void Cargar(TipoCargo tipocargo)
        {
            this.tipoCargo = tipocargo;
            //MessageBox.Show(tipoCargo.Categoria);
            tbxPuesto.Text = tipoCargo.Categoria;
        }

        private void btnContratar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
