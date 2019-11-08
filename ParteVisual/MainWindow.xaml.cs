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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ParteVisual
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BDTipoConcepto tc = new BDTipoConcepto();

            //List<TipoCargo> Ltp = tc.SelectTiposCargos();
            //List<TipoConcepto> Ltp = tc.SelectTiposConceptos();

            MessageBox.Show(tc.MaxIdDB().ToString());
            /*int id = Ltp[0].IdTipoCargo;

            Ltp[0].Categoria = "PRUEBA";

            tc.UpdateTipoCargo(Ltp[0]);
            Ltp = tc.SelectTiposCargos();
            MessageBox.Show(Ltp[30].ToString());
            //MessageBox.Show(tc.MaxIdDB().ToString());
            TipoCargo tipocargoNuevo = new TipoCargo(0, "CAtegoria Nueva4", 1200);*/

            //MessageBox.Show(tc.InsertTipoCargo(tipocargoNuevo).ToString());
            //MessageBox.Show(Ltp[0].IdTipoCargo.ToString());
            //MessageBox.Show(tc.DeleteTipoCargo(Ltp[0]).ToString());

            //Ltp = tc.SelectTiposCargos();
            //MessageBox.Show(Ltp[Ltp.Count() - 1].ToString());
            //MessageBox.Show(Ltp[0].ToString());



        }
    }
}
