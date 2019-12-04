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
using OfficeOpenXml;
using System.IO;
using Syncfusion.XlsIO;

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

            if(vistafmars.Aceptado)
            {
                bdrecibosueldo.InsertReciboSueldo(vistafmars.Recibosueldo);
                recibossueldos.Add(vistafmars.Recibosueldo);
                lstReciboSueldo.Items.Refresh();

                // Insert de conceptosrecibos nuevos
                int nuevoindice = bdconcepto.MaxIdDB();
                // Objetos necesarios
                List<Concepto> conceptosagregados = vistafmars.Conceptosagregados;
                ReciboSueldo recibosueldo = vistafmars.Recibosueldo;


                for (int i = 0; i < conceptosagregados.Count; i++)
                {
                    conceptosagregados[i].IdCR = conceptosagregados[i].IdCR + i;
                    bool sehizo = bdconcepto.InsertConcepto(conceptosagregados[i]);
                    if (!sehizo)
                    {
                        MessageBox.Show("No se ejecuto la query");
                    }
                    else MessageBox.Show("SI SE EJECUTO!");
                }
            }            
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            ReciboSueldo recibo = (ReciboSueldo)lstReciboSueldo.SelectedItem;
            if (recibo != null)
            {
                FAMReciboSueldo vistafamrecibo = new FAMReciboSueldo(recibo);
                //vistafamrecibo.Cargar(recibo);
                List<Concepto> quitar = vistafamrecibo.Conceptosquitados;
                vistafamrecibo.ShowDialog();

                if(vistafamrecibo.Aceptado)
                {
                    for (int i = 0; i < quitar.Count; i++)
                    {
                        bool res = bdconcepto.DeleteConcepto(quitar[i]);
                        if (res)
                        {
                            MessageBox.Show("ELIMINADO CON EXITO");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Seleccionó ningun recibo");
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            ReciboSueldo recibo = (ReciboSueldo)lstReciboSueldo.SelectedItem;
            if (recibo != null)
            {
                bdrecibosueldo.DeleteReciboSueldo(recibo);
                recibossueldos.Remove(recibo);
                lstReciboSueldo.ItemsSource = recibossueldos;
                lstReciboSueldo.Items.Refresh();
            }
            else
            {
                MessageBox.Show("No Seleccionó ningun recibo");
            }
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

        private void btnExportar_Click(object sender, RoutedEventArgs e)
        {
            ReciboSueldo recibo = (ReciboSueldo)lstReciboSueldo.SelectedItem;
            
            if (recibo != null)
            {
                Persona persona = bdpersona.SelectPersona(recibo.Legajo);
                String filename = @"reportes\\" + persona.Nombres + "-" + recibo.Mes.ToString() + "-" + recibo.Anio + ".csv";

                List<String> lineas = bdrecibosueldo.ToCSV(recibo);
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(filename))
                {
                    foreach (string line in lineas)
                    {
                        // If the line doesn't contain the word 'Second', write the line to the file.
                        if (!line.Contains("Second"))
                        {
                            file.WriteLine(line);
                        }
                    }
                }
                MessageBox.Show("CSV Creado con Exito!");
            }
            else
            {
                MessageBox.Show("No Seleccionó ningun recibo");
            }
        }

        private void btnExportarExcel_Click(object sender, RoutedEventArgs e)
        {
            //File.Delete(@"reportes\\recibosueldo.xls");
            ReciboSueldo recibo = (ReciboSueldo)lstReciboSueldo.SelectedItem;
            if (recibo != null)
            {
                Persona persona = bdpersona.SelectPersona(recibo.Legajo);
                String filename = @"reportes\\" + persona.Nombres + "-" + recibo.Mes.ToString() + "-" + recibo.Anio + ".xls";

                List<object[]> lineas = bdrecibosueldo.ToExcel(recibo);

                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IApplication application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2016;

                    //Reads input Excel stream as a workbook
                    //IWorkbook workbook = application.Workbooks.Open(File.OpenRead(System.IO.Path.GetFullPath(@"reportes\\recibosueldo.xls")));
                    IWorkbook workbook = application.Workbooks.Create();
                    IWorksheet sheet = workbook.Worksheets[0];

                    sheet.InsertRow(1, 1, ExcelInsertOptions.FormatAsBefore);
                    sheet.ImportArray(new object[]{ "Index","nombres","mes","anio","concepto","cantidad","monto "}, 1, 1, false);

                    for (int i=0; i < lineas.Count; i++)
                    {
                        sheet.InsertRow(i+2, 1, ExcelInsertOptions.FormatAsBefore);
                        sheet.ImportArray(lineas[i], i+2, 1, false);
                    }

                    //Save the file in the given path
                    Stream excelStream = File.Create(System.IO.Path.GetFullPath(filename));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();
                    MessageBox.Show("Excel Creado con Exito!");
                }
            }
            else
            {
                MessageBox.Show("No Seleccionó ningun recibo");
            }
        }
    }
}
