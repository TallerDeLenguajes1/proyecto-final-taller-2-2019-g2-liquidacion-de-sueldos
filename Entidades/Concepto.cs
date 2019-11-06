using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class Concepto
    {
        int idconcepto;
        string concept;
        float monto;
        public Concepto() { }    //Constructor por defecto

        public Concepto(int idconcepto, string concept, float monto)
        {
            this.idconcepto = idconcepto;
            this.concept = concept;
            this.monto = monto;
        }

        public int Idconcepto { get => idconcepto; set => idconcepto = value; }
        public string Concept { get => concept; set => concept = value; }
        public float Monto { get => monto; set => monto = value; }

        public override string ToString()
        {
            return idconcepto + "," + concept + "," + monto;
        }
    }
}
