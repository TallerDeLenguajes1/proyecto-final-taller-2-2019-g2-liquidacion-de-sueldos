using Entidades;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace AccesoDatos
{
    public class BDReciboSueldo
    {
        private List<ReciboSueldo> reciboSueldo;        
        Conexion conexion;
        Logger logger = LogManager.GetCurrentClassLogger();
        public BDReciboSueldo()
        {
            this.reciboSueldo = new List<ReciboSueldo>();            
            this.conexion = new Conexion();
        }
        /// <summary>
        /// Trae el maximo id de RecibosSueldos en la base de datos
        /// </summary>
        public int MaxIdDB()
        {
            int id = -1;
            try
            {
                const string qry = "SELECT max(idrs)+1 as idrs FROM recibossueldos;";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            id = Convert.ToInt32(rd["idrs"].ToString());
                        }
                    }
                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL SELECCIONAR MAXIMO ID DE RECIBOSSUELDOS ) -> {0}", ex.ToString());
            }
            return id;
        }
        /// <summary>
        /// Retorna una lista de RecibosSueldos
        /// </summary>
        public List<ReciboSueldo> SelectReciboSueldos()
        {
            try
            {
                const string qry = "SELECT * FROM recibossueldos WHERE baja != 1";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            reciboSueldo.Add(new ReciboSueldo
                            {
                                Idrs = Convert.ToInt32(rd["idrs"].ToString()),
                                Legajo = Convert.ToInt32(rd["legajo"].ToString()),
                                Mes = Convert.ToInt32(rd["mes"].ToString()),
                                Anio = Convert.ToInt32(rd["anio"].ToString()),
                                SueldoNeto = (float)Convert.ToDouble(rd["sueldoNeto"].ToString()),
                                SueldoBruto = (float)Convert.ToDouble(rd["sueldoBruto"].ToString())

                            });
                        }
                    }
                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL SELECCIONAR RECIBOSSUELDOS ) -> {0}", ex.ToString());
            }

            return reciboSueldo;
        }
        /// <summary>
        /// Retorna una lista de conceptos respectos de un reciboSueldo
        /// </summary>
        public List<Concepto> SelectConceptosRecibos(ReciboSueldo recibo)
        {
            List<Concepto> conceptos = new List<Concepto>();
            try
            {
                const string qry = "SELECT * FROM recibossueldos INNER JOIN conceptosrecibos USING (idRS) INNER JOIN conceptos USING(idConcepto) WHERE recibossueldos.baja != 1 AND conceptosrecibos.baja != 1 AND idRS = @idRS";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {                    
                    cmd.Parameters.AddWithValue("@idRS", recibo.Idrs);
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
                                Monto = (float)Convert.ToDouble(rd[10].ToString()),
                                Cantidad = (float)Convert.ToDouble(rd["cantidad"].ToString()),
                                TipoConcepto = new TipoConcepto
                                {
                                    IdTipoConcepto = Convert.ToInt32(rd["idConcepto"].ToString()),
                                    Concepto = rd["concepto"].ToString(),
                                    Monto = (float)Convert.ToDouble(rd[14].ToString())
                                }
                            });;
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
        /// Actualiza un ReciboSueldo en la base de datos
        /// </summary>
        public bool UpdateReciboSueldo(ReciboSueldo reciboSueldo)
        {
            bool estadoQry = false;
            try
            {
                string qry = "UPDATE recibossueldos SET idrs= @idrs, legajo= @legajo, mes= @mes, anio= @anio, sueldoBruto= @sueldoBruto WHERE idrs = @idrs";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idrs", reciboSueldo.Idrs);
                    cmd.Parameters.AddWithValue("@legajo", reciboSueldo.Legajo);
                    cmd.Parameters.AddWithValue("@mes", reciboSueldo.Mes);
                    cmd.Parameters.AddWithValue("@anio", reciboSueldo.Anio);
                    cmd.Parameters.AddWithValue("@sueldoNeto", reciboSueldo.SueldoNeto);
                    cmd.Parameters.AddWithValue("@SueldoBruto", reciboSueldo.SueldoBruto);
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
                logger.Error("ERROR!! ( AL ACTUALIZAR RECIBOSSUELDOS ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }
        /// <summary>
        /// Agrega un nuevo ReciboSueldo en la base de datos
        /// </summary>
        public bool InsertReciboSueldo(ReciboSueldo reciboSueldo)
        {
            bool estadoQry = false;
            int nuevoIdrs = MaxIdDB();
            try
            {
                string qry = "insert into recibossueldos (idrs,legajo,mes,anio,sueldoBruto,sueldoNeto) values (@idrs,@legajo, @mes,@anio,@sueldoBruto,@sueldoNeto)";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {

                    cmd.Parameters.AddWithValue("@legajo", reciboSueldo.Legajo);
                    cmd.Parameters.AddWithValue("@mes", reciboSueldo.Mes);
                    cmd.Parameters.AddWithValue("@anio", reciboSueldo.Anio);
                    cmd.Parameters.AddWithValue("@sueldoBruto", reciboSueldo.SueldoBruto);
                    cmd.Parameters.AddWithValue("@sueldoNeto", reciboSueldo.SueldoNeto);
                    cmd.Parameters.AddWithValue("@idrs", nuevoIdrs);
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
                logger.Error("ERROR!! ( AL AGREGAR NUEVO RECIBOSSUELDOS ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }
        /// <summary>
        /// Elimina un reciboSueldo en la base de datos
        /// </summary>
        public bool DeleteReciboSueldo(ReciboSueldo reciboSueldo)
        {
            bool estadoQry = false;
            try
            {
                //LA BASE DE DATOS NO TE DEJA ELIMINAR idrs, ES FOREING KEY EN ReciboSueldo
                string qry = "UPDATE recibossueldos SET baja = 1 WHERE idRS = @idRS";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idRS", reciboSueldo.Idrs);
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
                logger.Error("ERROR!! ( AL BORRAR RECIBOSSUELDOS ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }

        /// <summary>
        /// Retorna una lista de conceptos respectos de un reciboSueldo
        /// </summary>
        public List<String> ToCSV(ReciboSueldo recibo)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            List<String> lineas = new List<string>();
            List<Concepto> conceptos = new List<Concepto>();
            String linea = "Index,nombres,mes,anio,concepto,cantidad,monto";
            lineas.Add(linea);
            int contador = 1;
            try
            {
                const string qry = "SELECT * FROM personas INNER JOIN recibossueldos USING(legajo) INNER JOIN conceptosrecibos USING(idRS) INNER JOIN conceptos USING(idConcepto) WHERE recibossueldos.baja != 1 AND conceptosrecibos.baja != 1 AND personas.baja != 1 AND conceptos.baja != 1 AND idRS = @idRS";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idRS", recibo.Idrs);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            float monto = (float)(Convert.ToDouble(rd[16].ToString()));
                            float cantidad = (float)(Convert.ToDouble(rd["cantidad"].ToString()));
                            linea = contador + "," + rd["nombres"].ToString() + "," + rd["mes"].ToString() + "," + rd["anio"].ToString() + "," + rd["concepto"].ToString() + "," + cantidad.ToString("F",nfi) + "," + monto.ToString("F", nfi);
                            lineas.Add(linea);
                            contador++;
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

            return lineas;
        }

        /// <summary>
        /// Retorna una lista de conceptos respectos de un reciboSueldo
        /// </summary>
        public List<object[]> ToExcel(ReciboSueldo recibo)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            List<object[]> lineas = new List<object[]>();                                    
            int contador = 1;
            try
            {
                const string qry = "SELECT * FROM personas INNER JOIN recibossueldos USING(legajo) INNER JOIN conceptosrecibos USING(idRS) INNER JOIN conceptos USING(idConcepto) WHERE recibossueldos.baja != 1 AND conceptosrecibos.baja != 1 AND personas.baja != 1 AND conceptos.baja != 1 AND idRS = @idRS";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idRS", recibo.Idrs);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            String nombre = rd["nombres"].ToString();
                            int mes = Convert.ToInt32(rd["mes"].ToString());
                            int anio = Convert.ToInt32(rd["anio"].ToString());
                            String concepto = rd["concepto"].ToString();
                            float monto = (float)(Convert.ToDouble(rd[16].ToString()));
                            float cantidad = (float)(Convert.ToDouble(rd["cantidad"].ToString()));
                            lineas.Add(new object[] { contador, nombre, mes, anio, concepto, monto, cantidad });
                            contador++;
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

            return lineas;
        }


    }
}
