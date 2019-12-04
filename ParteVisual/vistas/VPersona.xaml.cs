using AccesoDatos;
using Entidades;
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

namespace ParteVisual.vistas
{
    /// <summary>
    /// Lógica de interacción para VPersona.xaml
    /// </summary>
    public partial class VPersona : Window
    {
        BDCargo bdCargo;
        BDPersona bdPersona;
        List<Persona> lstPersonas;
        Cargo cargo;
        public VPersona()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            bdPersona = new BDPersona();
            bdCargo = new BDCargo();
            lstPersonas = new List<Persona>();
            cargo = new Cargo();
            lstPersonas = bdPersona.SelectPersonas();
            this.lstbxPersona.ItemsSource = lstPersonas;

        }

        private void lstbxPersonas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.lstbxPersona.SelectedItem != null)
            {
                Persona personaSeleccionada = new Persona();
                personaSeleccionada = (Persona)this.lstbxPersona.SelectedItem;


            }
        }

        private void bntAgregarPersona_Click(object sender, RoutedEventArgs e)
        {
            FAMPersona famPersona = new FAMPersona();
            Persona pers = new Persona();
            
            famPersona.ShowDialog();
            

            pers = famPersona.Persona;
            bdPersona.InsertPersona(pers);
            lstPersonas.Add(pers);
            this.lstbxPersona.Items.Refresh();

        }

        private void bntModificarPersona_Click(object sender, RoutedEventArgs e)
        {
            if (lstbxPersona.SelectedItem != null)
            {
                FAMPersona famPersona = new FAMPersona();
                Persona SelectedTipoCargo = (Persona)lstbxPersona.SelectedItem;
                famPersona.Cargar(SelectedTipoCargo);
                famPersona.ShowDialog();
                //MessageBox.Show(SelectedTipoCargo.IdTipoCargo.ToString());

                bdPersona.UpdatePersona(famPersona.Persona);
                lstbxPersona.Items.Refresh();
                
                bdPersona.UpdatePersona(famPersona.Persona);
                this.lstbxPersona.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Debe selecionar una Persona válida");
            }
        }
    

        private void bntEliminarPersona_Click(object sender, RoutedEventArgs e)
        {
            Persona SelectedPersona = (Persona)lstbxPersona.SelectedItem;
            bdPersona.DeletePersona(SelectedPersona);
            lstPersonas.Remove(SelectedPersona);
            lstbxPersona.Items.Refresh();

            MessageBox.Show("Eliminado correcamente");
        }

        private void bntContratar_Click(object sender, RoutedEventArgs e)
        {
            if(lstbxPersona.SelectedItem != null)
            {
                FAMContratar famContratar = new FAMContratar();

                famContratar.Cargar((Persona)lstbxPersona.SelectedItem);
                famContratar.ShowDialog();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una persona válida");
            }

        }

        private void bntDespedir_Click(object sender, RoutedEventArgs e)
        {
            if (lstbxPersona.SelectedItem != null)
            {

                bdCargo.DeleteCargo(cargo);
                MessageBox.Show("Exito en la operación");
            }
            else
            {
                MessageBox.Show("Debe seleccionar una persona válida");
            }
        }
    }
}
