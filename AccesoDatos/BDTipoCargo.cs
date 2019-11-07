using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data.MySqlClient;

namespace AccesoDatos
{
    public class BDTipoCargo
    {
        private List<TipoCargo> tiposCargos;
        Conexion conexion;
        public BDTipoCargo()
        {
            this.tiposCargos = new List<TipoCargo>();
            this.conexion = new Conexion();
        }
        public List<TipoCargo> SelectCargos()
        {
            const string qry = "SELECT * FROM cargos";
            using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
            {
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        tiposCargos.Add(new TipoCargo { 
                            IdTipoCargo = Convert.ToInt32(rd["idcargo"].ToString()),
                            Categoria = rd["categoria"].ToString(),
                            SueldoBasico = (float)Convert.ToDouble(rd["sueldoBasico"].ToString())
                        });
                    }
                }
            }
            conexion.Desconectar();
            return tiposCargos;
        }
        public void UpdateCargo(Cargo cargo)
        {
        }


    }
}
