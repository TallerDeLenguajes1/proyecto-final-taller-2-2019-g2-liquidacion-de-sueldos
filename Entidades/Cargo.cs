using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cargo
    {
        private int idPC;
        private int idCargo;
        private int legajo;
        private string funcion;
        private DateTime fechaIngreso;
        private DateTime fechaBaja;
        private int antiguedad;
        private Cargo tipoCargo;
        public Cargo() { }//constructor por defecto

        public Cargo(int idPC, int idCargo, int legajo, string funcion, DateTime fechaIngreso, DateTime fechaBaja, int antiguedad, Cargo tipoCargo)
        {
            this.idPC = idPC;
            this.idCargo = idCargo;
            this.legajo = legajo;
            this.funcion = funcion;
            this.fechaIngreso = fechaIngreso;
            this.fechaBaja = fechaBaja;
            this.antiguedad = antiguedad;
            this.tipoCargo = tipoCargo;
        }

        public int IdPC { get => idPC; set => idPC = value; }
        public int IdCargo { get => idCargo; set => idCargo = value; }
        public int Legajo { get => legajo; set => legajo = value; }
        public string Funcion { get => funcion; set => funcion = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public DateTime FechaBaja { get => fechaBaja; set => fechaBaja = value; }
        public int Antiguedad { get => antiguedad; set => antiguedad = value; }
        public Cargo TipoCargo { get => tipoCargo; set => tipoCargo = value; }

                          
        //Funcion Antiguedad
        public int CalcularAntiguedad()
        {
            return DateTime.Today.Year - fechaIngreso.Year;
        }
        public override string ToString()
        {
            return idPC + "," + legajo + "," + IdCargo + "," + Funcion + "," + FechaIngreso + "," + FechaBaja;
        }
    }
}
