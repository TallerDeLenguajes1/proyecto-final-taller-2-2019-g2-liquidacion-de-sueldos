using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Persona
    {
        private int legajo;
        private string nombres;
        private string apellidos;
        private string documento;
        private DateTime fechaNacimiento;
        private string sexo;
        private string baja;
        private List<Cargo> cargos;
        private List<ReciboSueldo> recibos;

        public Persona() { }//constructor por defecto

        public Persona(int legajo, string nombres, string apellidos, string documento, DateTime fechaNacimiento, string sexo, string baja, List<Cargo> cargos, List<ReciboSueldo> recibos)
        {
            this.legajo = legajo;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.documento = documento;
            this.fechaNacimiento = fechaNacimiento;
            this.sexo = sexo;
            this.baja = baja;
            this.cargos = cargos;
            this.recibos = recibos;
        }
        public Persona(int legajo, string nombres, string apellidos, string documento, string sexo, DateTime fechaNacimiento)
        {
            this.legajo = legajo;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.documento = documento;
            this.sexo = sexo;
            this.fechaNacimiento = fechaNacimiento;
        }
        public Persona(int legajo, string nombres, string apellidos, string documento, string sexo)
        {
            this.legajo = legajo;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.documento = documento;
            this.sexo = sexo;
            this.baja = "N";
        }
        public int Legajo { get => legajo; set => legajo = value; }
        public string Nombres { get => nombres; set => nombres = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Documento { get => documento; set => documento = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public string Baja { get => baja; set => baja = value; }
        public List<Cargo> Cargos { get => cargos; set => cargos = value; }
        public List<ReciboSueldo> Recibos { get => recibos; set => recibos = value; }

        public override string ToString()
        {
            return  nombres + "," + apellidos;
        }
    }
}
