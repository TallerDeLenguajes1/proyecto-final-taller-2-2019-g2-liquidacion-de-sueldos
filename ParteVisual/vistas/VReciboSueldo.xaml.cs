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
    /// Lógica de interacción para VReciboSueldo.xaml
    /// </summary>
    public partial class VReciboSueldo : Window
    {
        BDTipoConcepto bdtipoconcepto = new BDTipoConcepto();
        BDPersona bdpersona = new BDPersona();
        BDConcepto bdconcepto = new BDConcepto();
        BDReciboSueldo bdrecibosueldo = new BDReciboSueldo();
        List<ReciboSueldo> recibossueldos = new List<ReciboSueldo>();
        List<Concepto> conceptos = new List<Concepto>();
        List<Persona> personas = new List<Persona>();
        public VReciboSueldo()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            recibossueldos = bdrecibosueldo.SelectReciboSueldos();
            personas = bdpersona.SelectPersonas();
            lstPersona.ItemsSource = personas;
            lstPersona.SelectedItem = 0;
        }

        private void bntAgregarRS_Click(object sender, RoutedEventArgs e)
        {
            Persona persona = (Persona)lstPersona.SelectedItem;
            FAMReciboSueldo vistafmars = new FAMReciboSueldo(persona);
            vistafmars.ShowDialog();
            bdrecibosueldo.InsertReciboSueldo(vistafmars.Recibosueldo);            
            recibossueldos.Add(vistafmars.Recibosueldo);
            lstReciboSueldo.Items.Refresh();

            // Insert de conceptosrecibos nuevos
            int nuevoindice = bdconcepto.MaxIdDB();
            // Objetos necesarios
            List<TipoConcepto> conceptosagregados = vistafmars.Conceptosagregados;
            List<float> cantidades = vistafmars.Cantidades;
            ReciboSueldo recibosueldo = vistafmars.Recibosueldo;
            

            Concepto nuevoconcepto = new Concepto();
            for (int i = 0; i < conceptosagregados.Count; i++)
            {
                nuevoconcepto.IdCR = nuevoindice + i;
                nuevoconcepto.IdConcepto = conceptosagregados[i].IdTipoConcepto;
                nuevoconcepto.IdRS = recibosueldo.Idrs;
                nuevoconcepto.Legajo = persona.Legajo;
                nuevoconcepto.Monto = conceptosagregados[i].Monto;
                nuevoconcepto.Cantidad = cantidades[i];
                bool sehizo = bdconcepto.InsertConcepto(nuevoconcepto);
                if (!sehizo)
                {
                    MessageBox.Show("No se ejecuto la query");
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Persona persona = (Persona)lstPersona.SelectedItem;
            FAMReciboSueldo ventanamodificar = new FAMReciboSueldo(persona);
        }

        private void lstReciboSueldo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ReciboSueldo recibo = (ReciboSueldo)lstReciboSueldo.SelectedItem;
            conceptos = bdrecibosueldo.SelectConceptosRecibos(recibo);
            lstConcepto.ItemsSource = conceptos;
            lstConcepto.Items.Refresh();
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Persona persona = (Persona)lstPersona.SelectedItem;
            recibossueldos = bdpersona.SelectPersonaRecibo(persona);
            lstReciboSueldo.ItemsSource = recibossueldos;
            lstReciboSueldo.Items.Refresh();
        }

        private void btnModificar_Click_1(object sender, RoutedEventArgs e)
        {
            ReciboSueldo recibo = (ReciboSueldo)lstReciboSueldo.SelectedItem;
            FAMReciboSueldo vistafamrecibo = new FAMReciboSueldo(recibo);
            //vistafamrecibo.Cargar(recibo);
            vistafamrecibo.ShowDialog();            
        }
    }
}
