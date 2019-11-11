using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ReciboSueldo
    {
        private int idRS;
        private int legajo;
        private int mes;
        private int anio;
        private float sueldoBruto;
        private float sueldoNeto;
        private List<Concepto> conceptos;

        public ReciboSueldo() { }
        public ReciboSueldo(int idRS, int legajo, int mes, int anio, float sueldoBruto, float sueldoNeto)
        {
            this.idRS = idRS;
            this.legajo = legajo;
            this.mes = mes;
            this.anio = anio;
            this.sueldoBruto = sueldoBruto;
            this.sueldoNeto = sueldoNeto;
            //this.conceptos = conceptos;
        }
        public int IdRS { get => idRS; set => idRS = value; }
        public int Legajo { get => legajo; set => legajo = value; }
        public int Mes { get => mes; set => mes = value; }
        public int Anio { get => anio; set => anio = value; }
        public float SueldoBruto { get => sueldoBruto; set => sueldoBruto = value; }
        public float SueldoNeto { get => sueldoNeto; set => sueldoNeto = value; }
       // public List<Concepto> Conceptos { get => conceptos; set => conceptos = value; }
       

        public override string ToString()
        {
            return idRS + "," + legajo + "," + mes + "," + anio + "," + sueldoBruto + "," + sueldoNeto;
        }
        public float calcularMonto()
        {
            float total = 0;
            foreach(Concepto concepto in conceptos)
            {
                total += concepto.Monto;
            }
            return total;
        }
    }
}
