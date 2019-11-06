using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cargo
    {
        private int idCargo;
        private string categoria;
        private float sueldoBasico;
        public Cargo() { }

        public Cargo(int idCargo, string categoria, float sueldoBasico)
        {
            this.idCargo = idCargo;
            this.categoria = categoria ?? throw new ArgumentNullException(nameof(categoria));
            this.sueldoBasico = sueldoBasico;
        }

        public int IdCargo { get => idCargo; set => idCargo = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public float SueldoBasico { get => sueldoBasico; set => sueldoBasico = value; }

        public override string ToString()
        {
            return categoria + "," + sueldoBasico;
        }
    }
}
