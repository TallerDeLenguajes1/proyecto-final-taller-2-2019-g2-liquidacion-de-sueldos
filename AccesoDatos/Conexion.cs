using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace AccesoDatos
{
    /// <summary>
    /// Clase para manejar las conexiones/desconexiones con  la base de datos
    /// </summary>
    public class Conexion
    {
        const string cadenaConexion = "server=127.0.0.1;uid=root;pwd=1234;database=salarios";
        MySqlConnection con;
        Logger logger = LogManager.GetCurrentClassLogger();

        public Conexion()
        {
            con = new MySqlConnection(cadenaConexion);
        }
        /// <summary>
        /// Conecta la base de datos y devuelve un objeto MySqlConnection
        /// </summary>
        public MySqlConnection Conectar()
        {
            try
            {
                if(con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                logger.Error("ERROR!! ( AL CONECTARSE A LA BASE DE DATOS ) -> {0}", ex.ToString());
                //Console.WriteLine(ex.Message);
            }
            return con;
        }
        /// <summary>
        /// Desconecta la base de datos
        /// </summary>
        public void Desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
