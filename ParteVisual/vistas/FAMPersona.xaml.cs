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
    /// Lógica de interacción para FAMPersona.xaml
    /// </summary>
    public partial class FAMPersona : Window
    {
        Persona persona;
        public FAMPersona()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            persona = new Persona();
        }

        public Persona Persona { get => persona; }

        public void Cargar(Persona persona)
        {
            this.rbtnHombre.IsEnabled = false;
            this.rbtnMujer.IsEnabled = false;
            tbxDocumento.IsEnabled = false;
            fechaNacimiento.IsEnabled = false;

            if(persona.Sexo == "M")
            {
                rbtnHombre.IsChecked = true;
            }
            else
            {
                rbtnHombre.IsChecked = false;
                rbtnMujer.IsChecked = true;
            }

            this.persona = persona;

            tbxNombre.Text = persona.Nombres;
            tbxApellido.Text = persona.Apellidos;
            tbxDocumento.Text = persona.Documento;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sexo = "M";
            //Controles
            if (tbxNombre == null || tbxNombre.Text == "")
            {
                MessageBox.Show("Este no puede estar vacio");
            }
            else if (tbxApellido == null || tbxApellido.Text == "")
            {
                MessageBox.Show("Este no puede estar vacio");
            }
            else if (tbxDocumento == null || tbxDocumento.Text == "")
            {
                MessageBox.Show("Este no puede estar vacio");
            }
            else if((fechaNacimiento.SelectedDate == null || fechaNacimiento.SelectedDate >= DateTime.Now) && fechaNacimiento.IsEnabled != false)
            {
                MessageBox.Show("Debe selccionar una fecha válida");
            }
            else
            {
                if ((bool)rbtnMujer.IsChecked)
                    sexo = "F";
                if(fechaNacimiento.IsEnabled != false)
                    persona = new Persona(0, tbxNombre.Text, tbxApellido.Text, tbxDocumento.Text, sexo, (DateTime)fechaNacimiento.SelectedDate);
                else
                {
                    persona.Nombres = tbxNombre.Text;
                    persona.Apellidos = tbxApellido.Text;
                }
                //MessageBox.Show(persona.FechaNacimiento.ToString("yyyy-MM-dd"));
                Close();
            }
        }
    }
}
