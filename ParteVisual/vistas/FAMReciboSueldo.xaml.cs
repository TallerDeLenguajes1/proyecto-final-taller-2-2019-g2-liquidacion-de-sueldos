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
        private List<Concepto> conceptos = new List<Concepto>();
        private List<Concepto> conceptosagregados = new List<Concepto>();
        private List<Concepto> conceptosquitados = new List<Concepto>();
        List<TipoConcepto> tiposconceptos = new List<TipoConcepto>();        
        
        

        // Nuevo ReciboSueldo   
        private ReciboSueldo recibosueldo = new ReciboSueldo();

        // Persona a quien pertenece el Recibo
        private Persona persona = new Persona();

        // Booleano para saber si se Acepto la Carga/Modificacion
        private bool aceptado = false;
        
        public ReciboSueldo Recibosueldo { get => recibosueldo; set => recibosueldo = value; }        
        public Persona Persona { get => persona; set => persona = value; }
        public List<Concepto> Conceptosagregados { get => conceptosagregados; set => conceptosagregados = value; }
        public List<Concepto> Conceptos { get => conceptos; set => conceptos = value; }
        public List<Concepto> Conceptosquitados { get => conceptosquitados; set => conceptosquitados = value; }
        public bool Aceptado { get => aceptado; set => aceptado = value; }

        public FAMReciboSueldo(Persona persona)
        {
            InitializeComponent();
            this.Persona = persona;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            tiposconceptos = bdtipoconcepto.SelectTiposConceptos();            
            lstTipoConcepto.ItemsSource = tiposconceptos;
            cldMesAnio.SelectedDate = DateTime.Now;            
            recibosueldo.Idrs = bdrecibosueldo.MaxIdDB();            
        }

        public FAMReciboSueldo(ReciboSueldo recibo)
        {
            InitializeComponent();
            DateTime fecha = Convert.ToDateTime("1/" + recibo.Mes + "/" + recibo.Anio);
            cldMesAnio.SelectedDate = fecha;
            Conceptos = bdrecibosueldo.SelectConceptosRecibos(recibo);
            Conceptosagregados = Conceptos;
            tiposconceptos = bdtipoconcepto.SelectTiposConceptos();
            lstTipoConcepto.ItemsSource = tiposconceptos;
            lstAgregados.ItemsSource = Conceptos;
            lstAgregados.Items.Refresh();
            persona = bdpersona.SelectPersona(recibo.Legajo);
        }

        /*public void Cargar(ReciboSueldo recibo)
        {
            DateTime fecha = Convert.ToDateTime("1/" + recibo.Mes + "/" + recibo.Anio);
            cldMesAnio.SelectedDate = fecha;
            Conceptos = bdrecibosueldo.SelectConceptosRecibos(recibo);
            lstAgregados.ItemsSource = Conceptos;
            lstAgregados.Items.Refresh();

        }*/

        private void lstAgregados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Concepto quitarconcepto = (Concepto) lstAgregados.SelectedItem;
            conceptosquitados.Add(quitarconcepto);
            Conceptosagregados.Remove(quitarconcepto);
            lstAgregados.ItemsSource = Conceptosagregados;
            lstAgregados.Items.Refresh();
        }

        private void btnagregarConcepto_Click(object sender, RoutedEventArgs e)
        {
            TipoConcepto nuevotipo = (TipoConcepto)lstTipoConcepto.SelectedItem;
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
                Concepto nuevoconcepto = new Concepto();
                nuevoconcepto.IdCR = bdconcepto.MaxIdDB();
                nuevoconcepto.IdConcepto = nuevotipo.IdTipoConcepto;
                nuevoconcepto.IdRS = recibosueldo.Idrs;
                nuevoconcepto.Legajo = persona.Legajo;
                nuevoconcepto.Monto = nuevotipo.Monto;
                nuevoconcepto.Cantidad = cantidad;
                nuevoconcepto.TipoConcepto = nuevotipo;

                Conceptosagregados.Add(nuevoconcepto);                
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
            // LLenando campos de ReciboSueldo
            recibosueldo.Legajo = this.Persona.Legajo;
            recibosueldo.Mes = cldMesAnio.SelectedDate.Value.Month; 
            recibosueldo.Anio = cldMesAnio.SelectedDate.Value.Year;

            // Calculando SueldoBruto
            float total = 0;
            for (int i = 0; i < Conceptosagregados.Count; i++)
            {
                total += Conceptosagregados[i].Monto;
            }
            recibosueldo.SueldoBruto = total;
            recibosueldo.SueldoNeto = total * 0.79f;

            aceptado = true;
            this.Close();            
        }
    }
}
