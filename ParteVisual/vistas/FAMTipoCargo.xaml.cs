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
    /// Lógica de interacción para FAMTipoCargo.xaml
    /// </summary>
    public partial class FAMTipoCargo : Window
    {
        TipoCargo tipoCargo;
        public FAMTipoCargo()
        {
            InitializeComponent();
            tipoCargo = new TipoCargo();
        }

        public TipoCargo TipoCargo { get => tipoCargo; }

        /// <summary>
        /// carga los datos correspondientes por defecto
        /// </summary>
        public void Cargar(TipoCargo tipocargo)
        {
            this.tipoCargo = tipocargo;
            tbxCategoria.Text = tipocargo.Categoria;
            tbxSueldoBase.Text = tipocargo.SueldoBasico.ToString();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //Controles
            float outParse;
            if (tbxCategoria.Text == null || tbxCategoria.Text == "")
            {
                MessageBox.Show("Este no puede estar vacio");
            }else if(!float.TryParse(tbxSueldoBase.Text, out outParse) || tbxSueldoBase.Text == null)
            {
                MessageBox.Show("Este campo debe ser numérico");
            }
            else
            {
                tipoCargo.Categoria = tbxCategoria.Text;
                tipoCargo.SueldoBasico = outParse;
                this.Close();
            }
        }

    }
}
