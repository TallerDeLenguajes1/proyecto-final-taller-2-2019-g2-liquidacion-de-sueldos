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
        
        public int Legajo { get => legajo; set => legajo = value; }
        public string Nombres { get => nombres; set => nombres = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Documento { get => documento; set => documento = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public string Baja { get => baja; set => baja = value; }

        public Persona() { }//constructor por defecto
        public Persona(int Legajo,string Nombres ,string Apellidos,string Documento,DateTime FechaNacimiento,string Sexo,string Baja)//constructor por parametros
        {
            Legajo = this.legajo;
            Nombres = this.nombres;
            Apellidos = this.apellidos;
            Documento = this.documento;
            FechaNacimiento = this.fechaNacimiento;
            Sexo = this.sexo;
            Baja = this.baja;
        }
     
        public override string ToString()
        {
            return legajo + "," + nombres + "," + apellidos + "," + documento + ","+ fechaNacimiento+""+ sexo + "," + baja;
        }
    }
}
