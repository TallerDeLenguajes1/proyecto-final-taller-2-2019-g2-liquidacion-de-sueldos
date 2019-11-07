using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TipoCargo
    {
        private int idTipoCargo;
        private string categoria;
        private float sueldoBasico;
        public TipoCargo() { }

        public TipoCargo(int idTipoCargo, string categoria, float sueldoBasico)
        {
            this.idTipoCargo = idTipoCargo;
            this.categoria = categoria ?? throw new ArgumentNullException(nameof(categoria));
            this.sueldoBasico = sueldoBasico;
        }

        public int IdTipoCargo { get => idTipoCargo; set => idTipoCargo = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public float SueldoBasico { get => sueldoBasico; set => sueldoBasico = value; }

        public override string ToString()
        {
            return categoria + "," + sueldoBasico;
        }
    }
}
