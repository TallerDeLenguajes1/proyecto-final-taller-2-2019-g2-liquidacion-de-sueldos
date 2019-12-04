using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TipoConcepto
    {
        int idTipoConcepto;
        string concepto;
        float monto;
        public TipoConcepto() { }    //Constructor por defecto
        public TipoConcepto(int idTipoConcepto, string concepto, float monto)
        {
            this.idTipoConcepto = idTipoConcepto;
            this.concepto = concepto;
            this.monto = monto;
        }

        public int IdTipoConcepto { get => idTipoConcepto; set => idTipoConcepto = value; }
        public string Concepto { get => concepto; set => concepto = value; }
        public float Monto { get => monto; set => monto = value; }

        public override string ToString()
        {
            return "Tipo: " + concepto + " Monto: " + monto;
        }
    }

}
