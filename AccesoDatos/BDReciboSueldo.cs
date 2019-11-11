using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    class BDReciboSueldo
    {
        private List<ReciboSueldo> reciboSueldo;
        Conexion conexion;
        public BDReciboSueldo()
        {
            this.reciboSueldo = new List<ReciboSueldo>();
            this.conexion = new Conexion();
        }
        //Trae el maximo id de la tabla ReciboSueldo de la base de datos
        private int MaxIdDB()
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
                Console.WriteLine(ex.Message);
            }
            return id;
        }
        public List<ReciboSueldo> SelectReciboSueldos()
        {
            try
            {
                const string qry = "SELECT * FROM recibossueldos";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            reciboSueldo.Add(new ReciboSueldo
                            {
                                IdRS = Convert.ToInt32(rd["idrs"].ToString()),
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
                Console.WriteLine(ex.Message);
            }

            return reciboSueldo;
        }
        public bool UpdateReciboSueldo(ReciboSueldo reciboSueldo)
        {
            bool estadoQry = false;
            try
            {
                string qry = "UPDATE recibossueldos SET idrs= @idrs, legajo= @legajo, mes= @mes, anio= @anio, sueldoBruto= @sueldoBruto WHERE idrs = @idrs";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idrs", reciboSueldo.IdRS);
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
                Console.WriteLine(ex.Message);
            }
            return estadoQry;
        }
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
                Console.WriteLine(ex.Message);
            }
            return estadoQry;
        }
        public bool DeleteReciboSueldo(ReciboSueldo reciboSueldo)
        {
            bool estadoQry = false;
            try
            {
                //LA BASE DE DATOS NO TE DEJA ELIMINAR idrs, ES FOREING KEY EN ReciboSueldo
                string qry = "DELETE FROM recibossueldos WHERE idrs = @idrs";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idrs", reciboSueldo.IdRS);
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
