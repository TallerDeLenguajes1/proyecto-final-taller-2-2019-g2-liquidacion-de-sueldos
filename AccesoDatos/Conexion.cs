﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class Conexion
    {
        const string cadenaConexion = "server=127.0.0.1;uid=root;pwd=1234;database=salarios";
        MySqlConnection con;
        
        public Conexion()
        {
            con = new MySqlConnection(cadenaConexion);
        }

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
                Console.WriteLine(ex.Message);
            }
            return con;
        }
        public void Desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
