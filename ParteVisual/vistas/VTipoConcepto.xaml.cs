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
    /// Lógica de interacción para VTipoConcepto.xaml
    /// </summary>
    public partial class VTipoConcepto : Window
    {
        List<TipoConcepto> tiposConceptos;
        BDTipoConcepto bdTipoConcepto;
        public VTipoConcepto()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            tiposConceptos = new List<TipoConcepto>();
            bdTipoConcepto = new BDTipoConcepto();
            tiposConceptos = bdTipoConcepto.SelectTiposConceptos();
            lstTiposConceptos.ItemsSource = tiposConceptos;
            lstTiposConceptos.SelectedItem = 0;
        }


        //Agrega un nuevo TipoConcepto
        private void btnAgregar_Click_1(object sender, RoutedEventArgs e)
        {
            FAMTipoConcepto famTipoConcepto = new FAMTipoConcepto();
            this.Hide();
            famTipoConcepto.ShowDialog();
            this.Show();

            bdTipoConcepto.InsertTipoConcepto(famTipoConcepto.TipoConcepto);
            famTipoConcepto.TipoConcepto.IdTipoConcepto = bdTipoConcepto.MaxIdDB() - 1;
            tiposConceptos.Add(famTipoConcepto.TipoConcepto);
            lstTiposConceptos.Items.Refresh();

        }
        /// Modifica un TipoConcepto que existe
        private void btnModificar_Click_1(object sender, RoutedEventArgs e)
        {
            if (lstTiposConceptos.SelectedItem != null)
            {
                FAMTipoConcepto fAMTipoConcepto = new FAMTipoConcepto();
                TipoConcepto SelectipoConcepto = (TipoConcepto)lstTiposConceptos.SelectedItem;
                fAMTipoConcepto.Cargar(SelectipoConcepto);
                fAMTipoConcepto.ShowDialog();
                bdTipoConcepto.UpdateTipoConcepto(fAMTipoConcepto.TipoConcepto);
                lstTiposConceptos.Items.Refresh();
            }
            else
            {
                MessageBox.Show("por favor seleccione un cargo valido");
            }
        }
        /// Baja lógica de un tipo concepto que existe
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            TipoConcepto SelectedTipoConcepto = (TipoConcepto)lstTiposConceptos.SelectedItem;
            bdTipoConcepto.DeleteTipoConcepto(SelectedTipoConcepto);
            MessageBox.Show("Eliminado correcamente");
            this.Close();

        }
    }
}
