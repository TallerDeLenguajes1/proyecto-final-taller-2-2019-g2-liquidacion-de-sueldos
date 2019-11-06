using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data.MySqlClient;

namespace AccesoDatos
{
    public class BDCargo
    {
        private List<Cargo> cargos;
        Conexion conexion;
        public BDCargo()
        {
            this.cargos = new List<Cargo>();
            this.conexion = new Conexion();
        }
        public List<Cargo> SelectCargos()
        {
            const string qry = "SELECT * FROM cargos";
            using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
            {
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        cargos.Add(new Cargo
                        {
                            IdCargo = Convert.ToInt32(rd["idCargo"].ToString()),
                            Categoria = rd["categoria"].ToString(),
                            SueldoBasico = (float)Convert.ToDouble(rd["sueldoBasico"].ToString())
                        });
                    }
                }
            }
            conexion.Desconectar();
            return cargos;
        }
        public void UpdateCargo(Cargo cargo)
        {
            /*
            const string qry = "UPDATE cargos SET categoria = @pcategoria, sueldobasico = @psueldobasico WHERE idcargo = @pidcargo";
            using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(MySql)
                {

                }
            }
            conexion.Desconectar();*/
        }


    }
}
