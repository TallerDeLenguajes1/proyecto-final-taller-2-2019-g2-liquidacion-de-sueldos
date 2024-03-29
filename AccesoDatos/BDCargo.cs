﻿using Entidades;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class BDCargo
    {
        private List<Cargo> cargos;
        Conexion conexion;
        Logger logger = LogManager.GetCurrentClassLogger();
        public BDCargo()
        {
            this.cargos = new List<Cargo>();
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
                const string qry = "SELECT max(idPC)+1 as idpc FROM personascargos;";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            id = Convert.ToInt32(rd["idpc"].ToString());
                        }
                    }
                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL SELECCIONAR MAXIMO ID DE PERSONASCARGOS ) -> {0}", ex.ToString());
            }
            return id;
        }
        /// <summary>
        /// Retorna una lista de objetos cargos
        /// </summary>
        public List<Cargo> SelectCargos()
        {
            try
            {
                const string qry = "SELECT * FROM personascargos WHERE baja != 1";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            cargos.Add(new Cargo
                            {
                                IdPC = Convert.ToInt32(rd["idPC"].ToString()),
                                Legajo = Convert.ToInt32(rd["legajo"].ToString()),
                                IdCargo = Convert.ToInt32(rd["idcargo"].ToString()),
                                Funcion= rd["funcion"].ToString(),
                                FechaIngreso = Convert.ToDateTime(rd["fechaIngreso"].ToString()),
                                FechaBaja = rd["fechaBaja"] == DBNull.Value ? Convert.ToDateTime("1990-1-1") 
                                    : Convert.ToDateTime(rd["fechaBaja"].ToString()),
                                Antiguedad = Convert.ToInt32(rd["antiguedad"].ToString())
                            });
                        }
                    }
                    conexion.Desconectar();
                }
            }
            catch (MySqlException ex)
            {
                //Console.WriteLine(ex.Message);
                logger.Error("ERROR!! ( AL SELECCIONAR PERSONASCARGOS ) -> {0}", ex.ToString());
            }

            return cargos;
        }
        /// <summary>
        /// Actualiza un cargo en la base de datos
        /// </summary>
        public bool UpdateCargo(Cargo cargo)
        {
            bool estadoQry = false;
            try
            {
                string qry = "UPDATE personascargos SET idPC= @idPC, idCargo= @idCargo, legajo = @legajo, funcion = @funcion, fechaIngreso = @fechaIngreso, fechaBaja = @fechaBaja, antiguedad = @antiguedad WHERE idPC = @idPC";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idPC", cargo.IdPC);
                    cmd.Parameters.AddWithValue("@idCargo", cargo.IdCargo);
                    cmd.Parameters.AddWithValue("@legajo", cargo.Legajo);
                    cmd.Parameters.AddWithValue("@funcion", cargo.Funcion);
                    cmd.Parameters.AddWithValue("@fechaIngreso", cargo.FechaIngreso);
                    cmd.Parameters.AddWithValue("@fechaBaja", cargo.FechaBaja);
                    cmd.Parameters.AddWithValue("@antiguedad", cargo.Antiguedad);
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
                logger.Error("ERROR!! ( AL ACTUALIZAR PERSONASCARGOS ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }
        /// <summary>
        /// Inserta un cargo nuevo en la base de datos
        /// </summary>
        public bool InsertCargo(Cargo cargo)
        {
            bool estadoQry = false;
            int nuevoId = MaxIdDB();
            try
            {
                string qry = "insert into personascargos (idPC, idCargo, legajo, funcion, fechaIngreso, fechaBaja, antiguedad) values (@idPC, @idCargo, @legajo, @funcion, @fechaIngreso, @fechaBaja, @antiguedad)";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idPC", cargo.IdPC);
                    cmd.Parameters.AddWithValue("@idCargo", cargo.IdCargo);
                    cmd.Parameters.AddWithValue("@legajo", cargo.Legajo);
                    cmd.Parameters.AddWithValue("@funcion", cargo.Funcion);
                    cmd.Parameters.AddWithValue("@fechaIngreso", cargo.FechaIngreso);
                    cmd.Parameters.AddWithValue("@fechaBaja", cargo.FechaBaja);
                    cmd.Parameters.AddWithValue("@antiguedad", cargo.Antiguedad);
                    if (cmd.ExecuteNonQuery() == 1)
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
                logger.Error("ERROR!! ( AL INSERTAR PERSONASCARGOS ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }
        /// <summary>
        /// Borra un cargo en la base de datos
        /// </summary>
        public bool DeleteCargo(Cargo cargo)
        {
            bool estadoQry = false;
            try
            {
                //LA BASE DE DATOS NO TE DEJA ELIMINAR CARGO, ES FOREING KEY EN PersonasCargos
                string qry = "DELETE FROM personascargos WHERE idPC = @idPC";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@idPC", cargo.IdPC);
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
                logger.Error("ERROR!! ( AL ELIMINAR PERSONASCARGOS ) -> {0}", ex.ToString());
            }
            return estadoQry;
        }
    }
}
