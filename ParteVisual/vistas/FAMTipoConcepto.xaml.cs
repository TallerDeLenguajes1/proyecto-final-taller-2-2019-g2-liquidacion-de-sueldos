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
    /// Lógica de interacción para FAMTipoConcepto.xaml
    /// </summary>
    public partial class FAMTipoConcepto : Window
    {
        TipoConcepto tipoConcepto;
        public FAMTipoConcepto()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            tipoConcepto = new TipoConcepto();
        }
        public TipoConcepto TipoConcepto { get => tipoConcepto; }
        /// carga los datos correspondientes por defecto
        public void Cargar(TipoConcepto tipoConcepto)
        {
            this.tipoConcepto = tipoConcepto;
            tbxConcepto.Text = tipoConcepto.Concepto;
            tbxmonto.Text = tipoConcepto.Monto.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //controles
            float fueradelugar;
            if (tbxConcepto.Text == null || tbxConcepto.Text == "")
            {
                MessageBox.Show("no puede ser vacio");
            }
            else if (!float.TryParse(tbxmonto.Text, out fueradelugar) || tbxmonto.Text == null)
            {
                MessageBox.Show("El campo debe ser numerico");
            }
            else
            {
                tipoConcepto.Concepto = tbxConcepto.Text;
                tipoConcepto.Monto = fueradelugar;
                this.Close();
            }
        }
    }
}
