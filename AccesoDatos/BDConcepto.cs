using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class BDConcepto
    {
        private List<Concepto> conceptos;
        Conexion conexion;
        public BDConcepto()
        {
            this.conceptos = new List<Concepto>();
            this.conexion = new Conexion();
        }
        //Trae el maximo id de la tabla cargos de la base de datos
        private int MaxIdDB()
        {
            int id = -1;
            try
            {
                const string qry = "SELECT max(idCR)+1 as idconcepto FROM conceptosrecibos;";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            id = Convert.ToInt32(rd["idconcepto"].ToString());
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
        
        public List<Concepto> SelectConceptos()
        {
            try
            {
                const string qry = "SELECT * FROM conceptosrecibos";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            conceptos.Add(new Concepto
                            {
                                IdCR = Convert.ToInt32(rd["idCR"].ToString()),
                                IdConcepto = Convert.ToInt32(rd["idConcepto"].ToString()),
                                IdRS = Convert.ToInt32(rd["idRS"].ToString()),
                                Legajo = Convert.ToInt32(rd["legajo"].ToString()),
                                Monto = (float)Convert.ToDouble(rd["monto"].ToString()),
                                Cantidad = (float)Convert.ToDouble(rd["cantidad"].ToString())
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

            return conceptos;
        }
        
        public bool UpdateConcepto(Concepto concepto)
        {
            bool estadoQry = false;
            try
            {
                string qry = "UPDATE conceptosrecibos SET idCR = @idCR, idConcepto = @idConepto, idRS = @idRS, legajo = @legajo, monto = @monto, cantidad = @cantidad WHERE idCR = @idCR";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idCR", concepto.IdCR);
                    cmd.Parameters.AddWithValue("@idConcepto", concepto.IdConcepto);
                    cmd.Parameters.AddWithValue("@idRS", concepto.IdRS);
                    cmd.Parameters.AddWithValue("@legajo", concepto.Legajo);
                    cmd.Parameters.AddWithValue("@monto", concepto.Monto);
                    cmd.Parameters.AddWithValue("@cantidad", concepto.Cantidad);
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
        public bool InsertConcepto(Concepto concepto)
        {
            bool estadoQry = false;
            int nuevoId = MaxIdDB();
            try
            {
                string qry = "insert into conceptosrecibos (idCR, idConcepto, idRS, legajo, monto, cantidad) values (@idCR, @idConepto, @idRS, @legajo, @monto, @cantidad)";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idCR", concepto.IdCR);
                    cmd.Parameters.AddWithValue("@idConcepto", concepto.IdConcepto);
                    cmd.Parameters.AddWithValue("@idRS", concepto.IdRS);
                    cmd.Parameters.AddWithValue("@legajo", concepto.Legajo);
                    cmd.Parameters.AddWithValue("@monto", concepto.Monto);
                    cmd.Parameters.AddWithValue("@cantidad", concepto.Cantidad);
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
        public bool DeleteConcepto(Concepto concepto)
        {
            bool estadoQry = false;
            try
            {
                //LA BASE DE DATOS NO TE DEJA ELIMINAR CARGO, ES FOREING KEY EN conceptosrecibos
                string qry = "DELETE FROM conceptosrecibos WHERE idCR = @idCR";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idCR", concepto.IdCR);
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
