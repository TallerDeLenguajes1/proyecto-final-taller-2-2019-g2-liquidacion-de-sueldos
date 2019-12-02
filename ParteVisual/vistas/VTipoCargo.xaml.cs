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
    /// Lógica de interacción para VTipoCargo.xaml
    /// </summary>
    public partial class VTipoCargo : Window
    {
        List<TipoCargo> tiposCargos;
        BDTipoCargo bdTipoCargo;
        public VTipoCargo()
        {
            InitializeComponent();
            tiposCargos = new List<TipoCargo>();
            bdTipoCargo = new BDTipoCargo();

            tiposCargos = bdTipoCargo.SelectTiposCargos();

            lstTiposCargos.ItemsSource = tiposCargos;
            lstTiposCargos.SelectedItem = 0;
        }
        /// <summary>
        /// Agrega un nuevo TipoCargo
        /// </summary>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            FAMTipoCargo famTipoCargo = new FAMTipoCargo();
            this.Hide();
            famTipoCargo.ShowDialog();
            this.Show();

            bdTipoCargo.InsertTipoCargo(famTipoCargo.TipoCargo);
            famTipoCargo.TipoCargo.IdTipoCargo = bdTipoCargo.MaxIdDB()-1;
            tiposCargos.Add(famTipoCargo.TipoCargo);
            lstTiposCargos.Items.Refresh();

        }
        /// <summary>
        /// Modifica un TipoCargo existente
        /// </summary>
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if(lstTiposCargos.SelectedItem != null)
            {
                FAMTipoCargo famTipoCargo = new FAMTipoCargo();
                TipoCargo SelectedTipoCargo = (TipoCargo)lstTiposCargos.SelectedItem;
                famTipoCargo.Cargar(SelectedTipoCargo);
                famTipoCargo.ShowDialog();
                //MessageBox.Show(SelectedTipoCargo.IdTipoCargo.ToString());

                bdTipoCargo.UpdateTipoCargo(famTipoCargo.TipoCargo);
                lstTiposCargos.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Debe selecionar un cargo válido");
            }
        }
        /// <summary>
        /// Baja lógica de un tipo cargo existente
        /// </summary>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            TipoCargo SelectedTipoCargo = (TipoCargo)lstTiposCargos.SelectedItem;
            bdTipoCargo.DeleteTipoCargo(SelectedTipoCargo);
            MessageBox.Show("Eliminado correcamente");
            this.Close();
            //lstTiposCargos.Items.Refresh();
            //lstTiposCargos.Items.Remove((TipoCargo)lstTiposCargos.SelectedItem);
            //ItemsControl.ItemsSource
        }
    }
}
