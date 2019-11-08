using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class BDTipoConcepto
    {
        private List<TipoConcepto> tiposConceptos;
        Conexion conexion;
        public BDTipoConcepto()
        {
            this.tiposConceptos = new List<TipoConcepto>();
            this.conexion = new Conexion();
        }
        //Trae el maximo id de la tabla cargos de la base de datos
        private int MaxIdDB()
        {
            int id = -1;
            try
            {
                const string qry = "SELECT max(idConcepto)+1 as idconcepto FROM conceptos;";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            id = Convert.ToInt32(rd["idConcepto"].ToString());
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
        public List<TipoConcepto> SelectTiposConceptos()
        {
            try
            {
                //const string qry = "SELECT * FROM conceptos WHERE monto <> \"NULL\"";
                const string qry = "SELECT * FROM conceptos";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            tiposConceptos.Add(new TipoConcepto
                            {
                                IdTipoConcepto = Convert.ToInt32(rd["idConcepto"].ToString()),
                                Concepto = rd["concepto"].ToString(),
                                Monto = (float)Convert.ToDouble(rd["monto"].ToString())
                             
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

            return tiposConceptos;
        }
        public bool UpdateTipoConcepto(TipoConcepto tipoConcepto)
        {
            bool estadoQry = false;
            try
            {
                string qry = "UPDATE conceptos SET concepto = @concepto, monto = @monto WHERE idconcepto = @idTipoConcepto";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@concepto", tipoConcepto.Concepto);
                    cmd.Parameters.AddWithValue("@monto", tipoConcepto.Monto);
                    cmd.Parameters.AddWithValue("@idTipoConcepto", tipoConcepto.IdTipoConcepto);
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
        public bool InsertTipoConcepto(TipoConcepto tipoConcepto)
        {
            bool estadoQry = false;
            int nuevoId = MaxIdDB();
            try
            {
                string qry = "insert into conceptos (idconcepto,concepto,monto) values (@idconcepo,@Concepto, @monto)";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@concepto", tipoConcepto.Concepto);
                    cmd.Parameters.AddWithValue("@monto", tipoConcepto.Monto);
                    cmd.Parameters.AddWithValue("@idConcepto", tipoConcepto.IdTipoConcepto);
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
        public bool DeleteTipoConcepto(TipoConcepto tipoConcepto)
        {
            bool estadoQry = false;
            try
            {
                //LA BASE DE DATOS NO TE DEJA ELIMINAR CARGO, ES FOREING KEY EN conceptos
                string qry = "DELETE FROM conceptos WHERE idconcepto = @idTipoConcepto";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idTipoConcepto", tipoConcepto.IdTipoConcepto);
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
    }
}
