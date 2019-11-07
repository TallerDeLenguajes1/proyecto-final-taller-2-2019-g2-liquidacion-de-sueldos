using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cargo
    {
        private int idpc;
        private int idCargo;
        private string funcion;
        private DateTime fechaIngreso;
        private DateTime fechaBaja;
        //funcion antiguedad
        public int Idpc { get => idpc; set => idpc = value; }
        public int IdCargo { get => idCargo; set => idCargo = value; }
        public string Funcion { get => funcion; set => funcion = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public DateTime FechaBaja { get => fechaBaja; set => fechaBaja = value; }

        public Cargo() { }//constructor por defecto
        public Cargo(int Idpc, int Legajo, string Funcion, DateTime FechaIngreso, DateTime FechaBaja)//constructor por parametros
        {
            Idpc = this.Idpc;
            Legajo = this.idCargo;
            Funcion = this.funcion;
            FechaIngreso = this.fechaIngreso;
            FechaBaja = this.fechaBaja;
        }
        public int Antiguedad()
        {
            return DateTime.Today.Year - FechaIngreso.Year;
        }
        public override string ToString()
        {
            return idpc + " - " + idCargo + " " + funcion + "  " + fechaIngreso + " " + fechaBaja;
        }
        public string ConcatenarDatos()
        {
            return idpc + ";" + idCargo + ";" + funcion + ";" + fechaIngreso + ";" + fechaBaja;
        }
    }
}
