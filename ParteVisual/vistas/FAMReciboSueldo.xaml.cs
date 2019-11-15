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
        BDTipoConcepto bdtipoconcepto = new BDTipoConcepto();
        BDPersona bdpersona = new BDPersona();
        BDConcepto bdconcepto = new BDConcepto();
        BDReciboSueldo bdrecibosueldo = new BDReciboSueldo();

        // Listas
        private List<Concepto> recibossueldos = new List<Concepto>();
        private List<TipoConcepto> conceptosagregados = new List<TipoConcepto>();
        private List<float> cantidades = new List<float>();
        List<TipoConcepto> tiposconceptos = new List<TipoConcepto>();        
        List<Persona> personas = new List<Persona>();
        

        // Nuevo ReciboSueldo   
        private ReciboSueldo recibosueldo = new ReciboSueldo();

        // Persona a quien pertenece el Recibo
        private Persona persona = new Persona();        
        
        public ReciboSueldo Recibosueldo { get => recibosueldo; set => recibosueldo = value; }
        public List<Concepto> Recibossueldos { get => recibossueldos; set => recibossueldos = value; }
        public Persona Persona { get => persona; set => persona = value; }
        public List<TipoConcepto> Conceptosagregados { get => conceptosagregados; set => conceptosagregados = value; }
        public List<float> Cantidades { get => cantidades; set => cantidades = value; }

        public FAMReciboSueldo()
        {
            InitializeComponent();
            tiposconceptos = bdtipoconcepto.SelectTiposConceptos();
            personas = bdpersona.SelectPersonas();
            lstTipoConcepto.ItemsSource = tiposconceptos;
            lstPersona.ItemsSource = personas;
            cldMesAnio.SelectedDate = DateTime.Now;
            this.Persona = null;
            recibosueldo.Idrs = bdrecibosueldo.MaxIdDB();            
        }
        
        private void lstAgregados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TipoConcepto quitarconcepto = (TipoConcepto) lstAgregados.SelectedItem;
            int indice = lstAgregados.Items.IndexOf(quitarconcepto);
            Conceptosagregados.Remove(quitarconcepto);
            Cantidades.Remove(Cantidades[indice]);
            lstAgregados.Items.Remove(quitarconcepto);
            lstAgregados.Items.Refresh();
            
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Persona = (Persona) lstPersona.SelectedItem;
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
                Conceptosagregados.Add(nuevoconcepto);
                Cantidades.Add(cantidad);
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
            if(this.Persona ==  null)
            {
                MessageBox.Show("No selecciono ninguna persona");
            }
            else
            {
                // LLenando campos de ReciboSueldo
                recibosueldo.Legajo = Persona.Legajo;
                recibosueldo.Mes = cldMesAnio.SelectedDate.Value.Month; 
                recibosueldo.Anio = cldMesAnio.SelectedDate.Value.Year;

                // Culculando SueldoBruto
                float total = 0;
                for (int i = 0; i < Conceptosagregados.Count; i++)
                {
                    total += Conceptosagregados[i].Monto;
                }
                recibosueldo.SueldoBruto = total;
                recibosueldo.SueldoNeto = total;

                this.Close();
            }
        }
    }
}
