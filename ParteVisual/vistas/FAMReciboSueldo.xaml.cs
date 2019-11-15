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
        List<float> cantidades = new List<float>();
        List<Persona> personas = new List<Persona>();
        private List<Concepto> recibossueldos =  new List<Concepto>();

        // Nuevo ReciboSueldo   
        private ReciboSueldo recibosueldo = new ReciboSueldo();

        // Persona a quien pertenece el Recibo
        Persona persona = new Persona();

        //Mes y anio para crear recibosueldo
        int mes;
        int anio;

        
        
        public ReciboSueldo Recibosueldo { get => recibosueldo; set => recibosueldo = value; }
        public List<Concepto> Recibossueldos { get => recibossueldos; set => recibossueldos = value; }

        public FAMReciboSueldo()
        {
            InitializeComponent();
            tiposconceptos = bdconcepto.SelectTiposConceptos();
            personas = bdpersona.SelectPersonas();
            lstTipoConcepto.ItemsSource = tiposconceptos;
            lstPersona.ItemsSource = personas;
            cldMesAnio.SelectedDate = DateTime.Now;
            this.persona = null;
            //recibosueldo.Mes = cldMesAnio.SelectedDate.Value.Month;
            //recibosueldo.Anio = cldMesAnio.SelectedDate.Value.Year;
        }
        
        private void lstAgregados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TipoConcepto quitarconcepto = (TipoConcepto) lstAgregados.SelectedItem;
            int indice = lstAgregados.Items.IndexOf(quitarconcepto);
            conceptosagregados.Remove(quitarconcepto);
            cantidades.Remove(cantidades[indice]);
            lstAgregados.Items.Remove(quitarconcepto);
            lstAgregados.Items.Refresh();
            
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            persona = (Persona) lstPersona.SelectedItem;
            MessageBox.Show("Persona Seleccionada");
            
        }

        private void btnagregarConcepto_Click(object sender, RoutedEventArgs e)
        {
            TipoConcepto nuevoconcepto = (TipoConcepto)lstTipoConcepto.SelectedItem;
            float cantidad = new float();
            float outParse;
            if (txtCantidad.Text == null || txtCantidad.Text == "")
            {
                MessageBox.Show("Este no puede estar vacio");
            }
            else if (!float.TryParse(txtCantidad.Text, out outParse) || txtCantidad.Text == null)
            {
                MessageBox.Show("Este campo debe ser numérico");
            }
            else
            {
                cantidad = outParse;
                conceptosagregados.Add(nuevoconcepto);
                cantidades.Add(cantidad);
                lstAgregados.Items.Add(nuevoconcepto);
                lstAgregados.Items.Refresh();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCrearRecibo_Click(object sender, RoutedEventArgs e)
        {
            if(this.persona ==  null)
            {
                MessageBox.Show("No selecciono ninguna persona");
            }
            else if (true)
            {

            }
        }
    }
}
