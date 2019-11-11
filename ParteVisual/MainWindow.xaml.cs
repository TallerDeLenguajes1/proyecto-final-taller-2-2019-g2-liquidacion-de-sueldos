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
            BDConcepto tc = new BDConcepto();

            //List<TipoCargo> Ltp = tc.SelectTiposCargos();
            List<Concepto> Ltp = tc.SelectConceptos();

            //MessageBox.Show(tc.MaxIdDB().ToString());
            MessageBox.Show(Ltp[5].ToString());
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

            BDPersona pr = new BDPersona();//declaro una instancia ,objeto de BDPersona 
            List<Persona> lpr = pr.SelectPersonas();//declaro una lista de la clase persona ,selecciono a las personas de la tabla
            MessageBox.Show(lpr[5].ToString());//muestro los valores de esa tabla
            int id = lpr[0].Legajo;
            lpr[0].Nombres = "PRUEBA";
            pr.UpdatePersona(lpr[0]);
            //lpr = pr.SelectTiposCargos();
            MessageBox.Show(lpr[30].ToString());
            //MessageBox.Show(tc.MaxIdDB().ToString());
           // Persona PersonaNueva = new Persona(39816, "facundo", "delgado", "32222222", 18 / 12 / 2019, "m", "f", List < Cargo > cargos);

        }
    }
}
