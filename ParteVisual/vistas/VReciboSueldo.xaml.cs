﻿using AccesoDatos;
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
        BDReciboSueldo bdrecibosueldo = new BDReciboSueldo();
        List<ReciboSueldo> recibossueldos = new List<ReciboSueldo>();
        public VReciboSueldo()
        {
            InitializeComponent();
            recibossueldos = bdrecibosueldo.SelectReciboSueldos();
            lstReciboSueldo.ItemsSource = recibossueldos;
            lstReciboSueldo.SelectedItem = 0;
        }

        private void bntAgregarRS_Click(object sender, RoutedEventArgs e)
        {
            FAMReciboSueldo vistafmars = new FAMReciboSueldo();
            vistafmars.ShowDialog();
        }
    }
}
