using Entidades;
using MySql.Data.MySqlClient;
using NLog;
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
        Logger logger = LogManager.GetCurrentClassLogger();
        public BDConcepto()
        {
            this.conceptos = new List<Concepto>();
            this.conexion = new Conexion();
        }
        /// <summary>
        /// Trae el maximo id de Conceptos en la base de datos
        /// </summary>
        public int MaxIdDB()
        {
            int id = -1;
            try
            {
                const string qry = "SELECT max(idCR)+1 as idcr FROM conceptosrecibos;";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            id = Convert.ToInt32(rd["idcr"].ToString());
                        }
                    }
                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL SELECCIONAR MAXIMO ID DE CONCEPTOSRECIBOS ) -> {0}", ex.ToString());
            }
            return id;
        }
        /// <summary>
        /// Retorna un lista de conceptos
        /// </summary>
        public List<Concepto> SelectConceptos()
        {
            try
            {
                const string qry = "SELECT * FROM conceptosrecibos INNER JOIN conceptos USING(idConcepto) WHERE conceptosrecibos.baja != 1 AND conceptos.baja != 1";
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
                                Cantidad = (float)Convert.ToDouble(rd["cantidad"].ToString()),
                                TipoConcepto = new TipoConcepto
                                {
                                    IdTipoConcepto = Convert.ToInt32(rd["idConcepto"].ToString()),
                                    Concepto = rd["concepto"].ToString(),
                                    Monto = (float)Convert.ToDouble(rd[14].ToString())
                                }
                            }); ;
                        }
                    }
                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL SELECCIONAR CONCEPTOSRECIBOS ) -> {0}", ex.ToString());
            }

            return conceptos;
        }
        /// <summary>
        /// Actualiza un concepto en la base de datos
        /// </summary>
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
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL ACTUALIZAR CONCEPTOSRECIBOS ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }
        /// <summary>
        /// Agrega un nuevo concepto en la base de datos
        /// </summary>
        public bool InsertConcepto(Concepto concepto)
        {
            bool estadoQry = false;
            int nuevoId = MaxIdDB();
            try
            {
                string qry = "insert into conceptosrecibos (idCR, idConcepto, idRS, legajo, monto, cantidad, baja) values (@idCR, @idConcepto, @idRS, @legajo, @monto, @cantidad,0)";
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
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL AGREGAR NUEVO CONCEPTOSRECIBOS ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }
        /// <summary>
        /// Elimina un concepto en la base de datos
        /// </summary>
        public bool DeleteConcepto(Concepto concepto)
        {
            bool estadoQry = false;
            try
            {
                //LA BASE DE DATOS NO TE DEJA ELIMINAR CARGO, ES FOREING KEY EN conceptosrecibos
                string qry = "UPDATE conceptosrecibos SET baja = 1 WHERE idCR = @idCR";
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
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL ELIMINAR CONCEPTOSRECIBOS ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }
    }
}
