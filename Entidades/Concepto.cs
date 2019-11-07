using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Concepto
    {
        private int idConcepto;
        private int idTipoConcepto;
        private int idrs;
        private int legajo;
        private float monto;
        private int cantidad;
        public Concepto() { }    //Constructor por defecto
        public Concepto(int idConcepto, int idTipoConcepto, int idrs, int legajo, float monto, int cantidad)
        {
            this.idConcepto = idConcepto;
            this.idTipoConcepto = idTipoConcepto;
            this.idrs = idrs;
            this.legajo = legajo;
            this.monto = monto;
            this.cantidad = cantidad;
        }

        public int IdConcepto { get => idConcepto; set => idConcepto = value; }
        public int IdTipoConcepto { get => idTipoConcepto; set => idTipoConcepto = value; }
        public int Idrs { get => idrs; set => idrs = value; }
        public int Legajo { get => legajo; set => legajo = value; }
        public float Monto { get => monto; set => monto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public override string ToString()
        {
            return idConcepto + "," + idTipoConcepto + "," + idrs + "," + legajo + "," + monto + "," + cantidad;
        }

    }
}
