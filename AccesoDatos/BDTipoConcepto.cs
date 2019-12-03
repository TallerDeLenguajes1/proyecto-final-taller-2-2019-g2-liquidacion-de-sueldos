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
    public class BDTipoConcepto
    {
        private List<TipoConcepto> tiposConceptos;
        Conexion conexion;
        Logger logger = LogManager.GetCurrentClassLogger();
        public BDTipoConcepto()
        {
            this.tiposConceptos = new List<TipoConcepto>();
            this.conexion = new Conexion();
        }
        /// <summary>
        /// Trae el maximo id de conceptos en la base de datos
        /// </summary>
        public int MaxIdDB()
        {
            int id = -1;
            try
            {
                const string qry = "SELECT max(idconcepto)+1 as idconcepto FROM conceptos;";
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
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL SELECCIONAR MAXIMO ID DE CONCEPTOS ) -> {0}", ex.ToString());
            }
            return id;
        }
        /// <summary>
        /// Retorna una lista de Tipo conceptos
        /// </summary>
        public List<TipoConcepto> SelectTiposConceptos()
        {
            try
            {
                float montoTmp;
                //const string qry = "SELECT * FROM conceptos WHERE monto <> \"NULL\"";
                const string qry = "SELECT * FROM conceptos WHERE baja != 1";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["monto"].ToString() != "")
                            {
                                montoTmp = float.Parse(rd["monto"].ToString());
                            }
                            else
                            {
                                montoTmp = float.Parse("0");
                            }
                            tiposConceptos.Add(new TipoConcepto
                            {

                                IdTipoConcepto = Convert.ToInt32(rd["idconcepto"].ToString()),
                                Concepto = rd["concepto"].ToString(),
                                Monto = montoTmp

                            });
                        }
                    }
                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL SELECCIONAR CONCEPTOS ) -> {0}", ex.ToString());
            }

            return tiposConceptos;
        }
        /// <summary>
        /// Actualiza un TipoConcepto en la base de datos
        /// </summary>
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
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL ACTUALIZAR CONCEPTOS ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }
        /// <summary>
        /// Agrega un nuevo tipoconcepto en la base de datos
        /// </summary>
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
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL INSERTAR CONCEPTO ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }
        /// <summary>
        /// Borra un tipo concepto en la base de datos 
        /// </summary>
        public bool DeleteTipoConcepto(TipoConcepto tipoConcepto)
        {
            bool estadoQry = false;
            try
            {
                //LA BASE DE DATOS NO TE DEJA ELIMINAR Concepto, ES FOREING KEY EN ConceptosRecibos
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
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL BORRAR CONCEPTO ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }
    }
}