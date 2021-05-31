using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;

namespace PruebaDialogo
{

    class Program {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Login");
                Console.Write("Seleccionar : ");
                string sec = Console.ReadLine();
                if (sec == "1")
                {
                    Conectar();
                }
                Console.Write("Continuar (s/n) : ");
                string onay = Console.ReadLine();
                if (onay != "s")
                {
                    break;
                }
            }
        }

        /*
        public static void getLogin()
        {
            string usuario;

            

            SqlCommand cmdSelect = null;
            SqlDataReader dr = null;

            Console.Write("Usuario : ");
            string nombre = Console.ReadLine();
            Console.Write("Password : ");
            string password = Console.ReadLine();

            cmd.CommandText = "SELECT nombre FROM Usuario where Usuario.nombre = '" + nombre + "' AND  Usuario.clave =  '" + password + "' ";
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (String.IsNullOrEmpty(reader[0].ToString()))
                {
                    usuario = reader[0].ToString();
                    Console.WriteLine("Usuario :" + reader[0]);
                }
            }
            con.Close();
        }
        */
        public static void Conectar()
        {
            string cnnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\camil\source\repos\PruebaDialogo\PruebaDialogo\BaseDatos.mdf;Integrated Security=True";
         
            SqlCommand cmdSelect = null;
            SqlDataReader dr = null;

            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnStr))
                {
                    cnn.Open();

                    Debug.WriteLine("Conectado...");

                    cmdSelect = new SqlCommand("Select * from Usuarios", cnn);

                    dr = cmdSelect.ExecuteReader();

                    while (dr.Read())
                    {
                        Debug.WriteLine(" Nombre " + dr["nombre"].ToString() + "Clave " + dr["clave"].ToString());

                    }

                    /*
                    dr.Close();

                    Console.Write("Ing. id que desea modificar : ");
                    int id = int.Parse(Console.ReadLine());

                    //Actualizar sin parámetros
                    SqlCommand cmdUpdate = new SqlCommand("Update clientes set clave = 9999 where id = " + id, cnn);

                    //Actualizar con parámetros
                    SqlCommand cmdDeleteParametros = new SqlCommand("Delete from clientes where id = @pid", cnn);
                    cmdDeleteParametros.Parameters.Add("pid", System.Data.SqlDbType.Int).Value = id;


                    Debug.WriteLine("Registros actualizados -> " + cmdUpdate.ExecuteNonQuery());
                    Debug.WriteLine("Registros eliminados -> " + cmdDeleteParametros.ExecuteNonQuery());

                    */
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en la conexión " + ex.ToString());
            }
            finally
            {
                cmdSelect.Dispose();
                dr.Close();
            }
        }

    }
}
