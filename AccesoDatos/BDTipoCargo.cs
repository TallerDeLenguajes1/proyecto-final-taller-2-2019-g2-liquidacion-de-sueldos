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
        /// <summary>
        /// Trae el maximo id de cargos
        /// </summary>
        private int MaxIdDB()
        {
            int id = -1;
            try
            {
                const string qry = "SELECT max(idcargo)+1 as idcargo FROM cargos;";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            id = Convert.ToInt32(rd["idcargo"].ToString());
                        }
                    }
                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }
        /// <summary>
        /// Retorna una lista con todos los cargos
        /// </summary>
        public List<TipoCargo> SelectTiposCargos()
        {
            try
            {
                const string qry = "SELECT * FROM cargos";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            tiposCargos.Add(new TipoCargo
                            {
                                IdTipoCargo = Convert.ToInt32(rd["idcargo"].ToString()),
                                Categoria = rd["categoria"].ToString(),
                                SueldoBasico = (float)Convert.ToDouble(rd["sueldoBasico"].ToString())
                            });
                        }
                    }
                    conexion.Desconectar();
                }
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return tiposCargos;
        }
        /// <summary>
        /// Actualiza un Cargo en la base de datos
        /// </summary>
        public bool UpdateTipoCargo(TipoCargo tipoCargo)
        {
            bool estadoQry = false;
            try
            {
                string qry = "UPDATE cargos SET categoria = @categoria, sueldoBasico = @sueldoBasico WHERE idcargo = @idTipoCargo";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@categoria",tipoCargo.Categoria);
                    cmd.Parameters.AddWithValue("@sueldoBasico", tipoCargo.SueldoBasico);
                    cmd.Parameters.AddWithValue("@idTipoCargo", tipoCargo.IdTipoCargo);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        estadoQry = true;
                    }
                    else
                    {
                        estadoQry = false;
                    }

                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return estadoQry;
        }
        /// <summary>
        /// Agrega un nuevo cargo en la base de datos
        /// </summary>
        public bool InsertTipoCargo(TipoCargo tipoCargo)
        {
            bool estadoQry = false;
            int nuevoId = MaxIdDB();
            try
            {
                string qry = "insert into cargos (idcargo,categoria,sueldoBasico) values (@idcargo,@Categoria, @sueldoBasico)";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@categoria", tipoCargo.Categoria);
                    cmd.Parameters.AddWithValue("@sueldoBasico", tipoCargo.SueldoBasico);
                    cmd.Parameters.AddWithValue("@idCargo", nuevoId);
                    if(cmd.ExecuteNonQuery() == 1)
                    {
                        estadoQry = true;
                    }
                    else
                    {
                        estadoQry = false;
                    }
                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return estadoQry;
        }
        /// <summary>
        /// Realiza una baja logica de un cargo en la base de datos
        /// </summary>
        public bool DeleteTipoCargo(TipoCargo tipoCargo)
        {
            bool estadoQry = false;
            try
            {
                //LA BASE DE DATOS NO TE DEJA ELIMINAR CARGO, ES FOREING KEY EN CargosPersonas
                string qry = "DELETE FROM cargos WHERE idcargo = @idTipoCargo";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idTipoCargo", tipoCargo.IdTipoCargo);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        estadoQry = true;
                    }
                    else
                    {
                        estadoQry = false;
                    }

                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return estadoQry;
        }
        /// <summary>
        /// Retorna una lista de personas que ocuparon u ocupan un determinado cargo
        /// </summary>
        public List<Cargo> SelectPersonasCargos(TipoCargo tipocargo)
        {
            List<Cargo> personasCargos = new List<Cargo>();
            try
            {
                const string qry = "select * from personascargos inner join personas using (legajo) inner join cargos using (idCargo) where idCargo = @idTipoCargo;";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idTipoCargo", tipocargo.IdTipoCargo);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            personasCargos.Add(new Cargo
                            {
                                IdPC = Convert.ToInt32(rd["idPC"].ToString()),
                                IdCargo = Convert.ToInt32(rd["idcargo"].ToString()),
                                Legajo = Convert.ToInt32(rd["legajo"].ToString()),
                                Funcion = rd["legajo"].ToString(),
                                FechaIngreso = Convert.ToDateTime(rd["fechaIngreso"].ToString()),
                                FechaBaja = DateTime.MinValue,
                                Antiguedad = Convert.ToInt32(rd["antiguedad"].ToString()),
                                TipoCargo = tipocargo
                            });
                        }
                    }
                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return personasCargos;
        }
    }
}
