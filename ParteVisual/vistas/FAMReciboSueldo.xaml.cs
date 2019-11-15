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
    /// Lógica de interacción para FAMReciboSueldo.xaml
    /// </summary>
    public partial class FAMReciboSueldo : Window
    {
        // Clases de Acceso a Datos
        BDTipoConcepto bdconcepto = new BDTipoConcepto();
        BDPersona bdpersona = new BDPersona();

        // Listas
        List<TipoConcepto> tiposconceptos = new List<TipoConcepto>();
        List<TipoConcepto> conceptosagregados = new List<TipoConcepto>();
        List<Persona> personas = new List<Persona>();

        // Nuevo ReciboSueldo   
        ReciboSueldo recibosueldo = new ReciboSueldo();

        // Persona a quien pertenece el Recibo
        Persona persona = new Persona();

        public Persona Persona { get => persona; set => persona = value; }

        public FAMReciboSueldo()
        {
            InitializeComponent();
            tiposconceptos = bdconcepto.SelectTiposConceptos();
            personas = bdpersona.SelectPersonas();
            lstTipoConcepto.ItemsSource = tiposconceptos;
            lstPersona.ItemsSource = personas;
            cldMesAnio.SelectedDate = DateTime.Now;
            //recibosueldo.Mes = cldMesAnio.SelectedDate.Value.Month;
            //recibosueldo.Anio = cldMesAnio.SelectedDate.Value.Year;
        }

        private void lstTipoConcepto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TipoConcepto nuevoconcepto = (TipoConcepto) lstTipoConcepto.SelectedItem;
            lstAgregados.Items.Add(nuevoconcepto);
            lstAgregados.Items.Refresh();
            conceptosagregados.Add(nuevoconcepto);
        }

        private void lstAgregados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TipoConcepto quitarconcepto = (TipoConcepto) lstAgregados.SelectedItem;
            lstAgregados.Items.Remove(quitarconcepto);
            lstAgregados.Items.Refresh();
            conceptosagregados.Remove(quitarconcepto);
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            persona = (Persona) lstPersona.SelectedItem;
            MessageBox.Show("Persona Seleccionada");
            
        }

        private void btnagregarConcepto_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
