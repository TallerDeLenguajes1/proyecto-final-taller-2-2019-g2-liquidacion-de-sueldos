using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TipoCargo
    {
        private int idcr;
        private float cantidad;
        private float monto;
        private List<Concepto> conceptos;

        public ConceptoRecibo(int idcr, float cantidad, float monto, List<Concepto> conceptos)
        {
            this.idcr = idcr;
            this.cantidad = cantidad;
            this.monto = monto;
            this.conceptos = conceptos;
        }

        public int Idcr { get => idcr; set => idcr = value; }
        public float Cantidad { get => cantidad; set => cantidad = value; }
        public float Monto { get => monto; set => monto = value; }
        internal List<Concepto> Conceptos { get => conceptos; set => conceptos = value; }
    }
}
