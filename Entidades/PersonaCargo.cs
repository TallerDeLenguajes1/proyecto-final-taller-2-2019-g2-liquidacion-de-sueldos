using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class PersonaCargo
    {
        int idpc;
        int idCargo;
        string funcion;
        DateTime fechaIngreso;
        DateTime fechaBaja;
        //funcion antiguedad
        public int Idpc { get => idpc; set => idpc = value; }
        public int IdCargo { get => idCargo; set => idCargo = value; }
        public string Funcion { get => funcion; set => funcion = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public DateTime FechaBaja { get => fechaBaja; set => fechaBaja = value; }

        public PersonaCargo() { }//constructor por defecto
        public PersonaCargo(int Idpc, int Legajo, string Funcion, DateTime FechaIngreso, DateTime FechaBaja)//constructor por parametros
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
