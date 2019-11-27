using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class BDPersona
    {
        private List<Persona> persona;
        Conexion conexion;
        public BDPersona()
        {
            this.persona = new List<Persona>();
            this.conexion = new Conexion();
        }
        //Trae el maximo id de la tabla Personas de la base de datos
        private int MaxIdDB()
        {
            int id = -1;
            try
            {
                const string qry = "SELECT max(legajo)+1 as legajo FROM personas;";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            id = Convert.ToInt32(rd["legajo"].ToString());
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
        public List<Persona> SelectPersonas()
        {
            try
            {
                const string qry = "SELECT * FROM personas";
                using (var cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            persona.Add(new Persona
                            {
                                Legajo = Convert.ToInt32(rd["legajo"].ToString()),
                                Nombres = rd["nombres"].ToString(),
                                Apellidos = rd["apellidos"].ToString(),
                                Documento = rd["documento"].ToString(),
                                Sexo = rd["sexo"].ToString(),
                                Baja = rd["baja"].ToString()
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

            return persona;
        }
        public bool UpdatePersona(Persona persona)
        {
            bool estadoQry = false;
            try
            {
                string qry = "UPDATE PERSONA SET legajo = @legajo, nombres= @nombres, apellidos= @apellidos, documento=@documento, fechaNacimiento= @fechaNacimiento, sexo= @sexo, baja= @baja WHERE legajo = @legajo";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@legajo", persona.Legajo);
                    cmd.Parameters.AddWithValue("@nombres", persona.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", persona.Apellidos);
                    cmd.Parameters.AddWithValue("@documento", persona.Documento);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", persona.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@sexo", persona.Sexo);
                    cmd.Parameters.AddWithValue("@baja", persona.Baja);
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
        public bool InsertPersona(Persona persona)
        {
            bool estadoQry = false;
            int nuevoLegajo = MaxIdDB();
            try
            {
                string qry = "insert into Persona (legajo,nombres,apellidos,documento,fechaNacimiento,sexo,baja) values (@legajo,@nombres, @apellidos,@documento,@fechaNacimiento,@sexo,@baja)";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@nombres", persona.Nombres);
                    cmd.Parameters.AddWithValue("@aepellido", persona.Apellidos);
                    cmd.Parameters.AddWithValue("@documento", persona.Documento);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", persona.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@sexo", persona.Sexo);
                    cmd.Parameters.AddWithValue("@baja", persona.Baja);
                    cmd.Parameters.AddWithValue("@legajo", nuevoLegajo);
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
        public bool DeletePersona(Persona persona)
        {
            bool estadoQry = false;
            try
            {
                //LA BASE DE DATOS NO TE DEJA ELIMINAR legajo, ES FOREING KEY EN Personas
                string qry = "DELETE FROM personas WHERE legajo = @legajo";
                using (MySqlCommand cmd = new MySqlCommand(qry, conexion.Conectar()))
                {
                    cmd.Parameters.AddWithValue("@legajo", persona.Legajo);
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
