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
        List<TipoCargo> SelecCargo;
        Persona persona;
        BDTipoCargo bdTipoCargo;
        Cargo cargo;

        public FAMContratar()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


            bdTipoCargo = new BDTipoCargo();
            cargo = new Cargo();
            SelecCargo = new List<TipoCargo>();
            SelecCargo = bdTipoCargo.SelectTiposCargos();
            cbxPersonas.ItemsSource = SelecCargo;

        }
        public void Cargar(Persona persona)
        {
            this.persona = persona;

            tbxNombre.Text = persona.Nombres;
            tbxApellido.Text = persona.Apellidos;
            tbxDocumento.Text = persona.Documento;
            if (persona.Sexo == "M")
            {
                rbtnHombre.IsChecked = true;
            }
            else
            {
                rbtnHombre.IsChecked = false;
                rbtnMujer.IsChecked = true;
            }
            tbxApellido.IsEnabled = false;
            tbxNombre.IsEnabled = false;
            tbxDocumento.IsEnabled = false;
            rbtnHombre.IsEnabled = false;
            rbtnMujer.IsEnabled = false;
            fechaNacimiento.IsEnabled = false;
        }

        private void btnContratar_Click(object sender, RoutedEventArgs e)
        {
            cargo.Legajo = persona.Legajo;
            cargo.FechaIngreso = DateTime.Now;
            cargo.Funcion = ((TipoCargo)cbxPersonas.SelectedItem).Categoria;
            BDCargo bdCargo = new BDCargo();

            bdCargo.InsertCargo(cargo);
            this.Close();
        }
    }
}
