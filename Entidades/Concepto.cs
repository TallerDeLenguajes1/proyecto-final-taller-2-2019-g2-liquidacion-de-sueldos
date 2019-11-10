using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Concepto
    {
        private int idCR;
        private int idConcepto;
        private int idTipoConcepto;
        private int idRS;
        private int legajo;
        private float monto;
        private float cantidad;
        private Concepto tipoConcepto;
        public Concepto() { }    //Constructor por defecto

        public Concepto(int idCR, int idConcepto, int idTipoConcepto, int idRS, int legajo, float monto, float cantidad, Concepto tipoConcepto)
        {
            this.idCR = idCR;
            this.idConcepto = idConcepto;
            this.idTipoConcepto = idTipoConcepto;
            this.idRS = idRS;
            this.legajo = legajo;
            this.monto = monto;
            this.cantidad = cantidad;
            this.tipoConcepto = tipoConcepto;
        }

        public int IdCR { get => idCR; set => idCR = value; }
        public int IdConcepto { get => idConcepto; set => idConcepto = value; }
        public int IdTipoConcepto { get => idTipoConcepto; set => idTipoConcepto = value; }
        public int IdRS { get => idRS; set => idRS = value; }
        public int Legajo { get => legajo; set => legajo = value; }
        public float Monto { get => monto; set => monto = value; }
        public float Cantidad { get => cantidad; set => cantidad = value; }
        public Concepto TipoConcepto { get => tipoConcepto; set => tipoConcepto = value; }

        public override string ToString()
        {
            return idCR + "," + idConcepto + "," + idTipoConcepto + "," + idRS + "," + legajo + "," + monto + "," + cantidad;
        }

    }
}
