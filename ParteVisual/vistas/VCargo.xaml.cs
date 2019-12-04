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
    /// Lógica de interacción para VCargo.xaml
    /// </summary>
    public partial class VCargo : Window
    {

        BDTipoCargo dbTipoCargo;
        TipoCargo tipoCargoSeleccionado;
        public VCargo()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            dbTipoCargo = new BDTipoCargo();
            List<TipoCargo> SelectTipoCargos = new List<TipoCargo>();
            SelectTipoCargos = dbTipoCargo.SelectTiposCargos();
            lstbxCargos.ItemsSource = SelectTipoCargos;
            lstbxCargos.Items.Refresh();




            /*TipoCargo tp = new TipoCargo(6, "", 0);
            List<Cargo> SelectCargos = new List<Cargo>();
            SelectCargos = dbTipoCargo.SelectPersonasCargos(tp);
            lstbxCargos.ItemsSource = SelectCargos;
            MessageBox.Show(SelectCargos[0].ToString());
            lstbxCargos.Items.Refresh();
            */
        }

        private void lstbxCargos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tipoCargoSeleccionado = (TipoCargo)lstbxCargos.SelectedItem;
            //MessageBox.Show(tipoCargoSeleccionado.ToString());
            lstbxContrataciones.ItemsSource = dbTipoCargo.SelectPersonasCargos(tipoCargoSeleccionado);
            lstbxContrataciones.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*FAMContratar famContratar = new FAMContratar();
            this.Hide();
            famContratar.Cargar(tipoCargoSeleccionado);
            famContratar.ShowDialog();
            this.Show();*/
        }
    }
}
