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

        private static void Main(string[] args)
        {
            //inicializacion de objeto usuario
            Object[] usuario = new Object[3] { "", "", false };

            while (true)
            {
                Console.WriteLine("Presiona 1 para iniciar sesión");
                Console.Write("Seleccionar : ");
                string sec = Console.ReadLine();
                if (sec == "1") {
                    //Paso por parámetro usuario para instanciar objeto
                    Login(usuario);
                   
                    if ((bool)usuario[2])
                    {
                        Casos(usuario);
                    }
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
        private static void Login(Object[] usuario) {

            string cnnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\camil\source\repos\PruebaDialogo\PruebaDialogo\BaseDatos.mdf;Integrated Security=True";
         
            SqlCommand cmdSelect = null;
            SqlDataReader dr = null;
            Boolean retorno = false;

            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnStr))
                {
                    cnn.Open();

                    Console.Write("Ingresa nombre de usuario : ");
                    string nombre = Console.ReadLine();
                    Console.Write("Ingresa contraseña : ");
                    string password = Console.ReadLine();

                    cmdSelect = new SqlCommand("SELECT Usuario.nombre AS nombre, Rol.tipo AS tipo FROM Usuario, Rol where Usuario.nombre = '" + nombre + "' AND  Usuario.clave =  '" + password + "' AND Rol.rol_id = Usuario.rol_id", cnn);

                    dr = cmdSelect.ExecuteReader();

                    //Asignación de valores al objeto bajo referencia de memoria
                    while (dr.Read())
                    {
                        usuario[0] = dr["nombre"].ToString();
                        usuario[1] = dr["tipo"].ToString();
                        usuario[2] = true;
                        retorno = true;
                        Console.WriteLine("Bienvenido(a) " + dr["nombre"].ToString());
                        Console.WriteLine("Selecciona un número del menú");
                    }

                    if (!retorno)
                    {
                        Console.WriteLine("Usuario o clave incorrecto");
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
                Console.WriteLine("Error en la conexión " + ex.ToString());
            }
            finally

            {
                cmdSelect.Dispose();
                dr.Close();

            }
          
        }

        private static void Casos(Object[] usuario) {
            while (true)
            {
                Console.WriteLine("1. ARRIENDO DE AUTOS (E-S)");
                Console.WriteLine("2. AGREGAR DIAS CLIENTE (E-S)");
                Console.WriteLine("3. DEVOLUCIÓN DE AUTOS (E-S)");
                Console.WriteLine("4. ANULACIÓN DE ARRIENDOS (S)");
                Console.WriteLine("5. REPORTES SUPERVISOR (S)");
                Console.WriteLine("6. REPORTES Y CONSULTAS GENERALES (E)");
                Console.Write("Seleccionar : ");
                string sec = Console.ReadLine();
                if (sec == "1")
                {
                    ArriendoVehiculo(usuario);
                }
                if (sec == "2")
                {
                    ActualizacionDias(usuario);
                }
                Console.Write("Continuar (s/n) : ");
                string onay = Console.ReadLine();
                if (onay != "s")
                {
                    break;
                }
            }
        }

        private static void ArriendoVehiculo(Object[] usuario) {

            string cnnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\camil\source\repos\PruebaDialogo\PruebaDialogo\BaseDatos.mdf;Integrated Security=True";

            SqlCommand cmdSelect = null;
            SqlDataReader dr = null;
            int count = 0;
            int count2 = 0;
            Object[] cliente = new Object[50];
            Object[,] vehiculo = new Object[50, 2];

            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnStr))

                {
                    cnn.Open();
                    
                    //Console.Write("Password : ");
                    //string password = Console.ReadLine();

                    cmdSelect = new SqlCommand("SELECT rut, nombre, telefono FROM Cliente", cnn);

                    dr = cmdSelect.ExecuteReader();

                    //Asignación de valores al objeto bajo referencia de memoria

                    while (dr.Read()) {
                        Console.WriteLine("Clientes");
                        Console.WriteLine(count + 1 + ") Rut: " + dr["rut"].ToString() + " Nombre: " + dr["nombre"].ToString() + " Telefono: " + dr["telefono"].ToString());
                        cliente[count] = dr["rut"].ToString();
                        count++;
                    }
                    dr.Close();
                    if (count > 0) {
                        Console.Write("Seleccione el número del cliente : ");
                        string nCliente = Console.ReadLine();
                        if (Int32.Parse(nCliente) <= count) {
                            cmdSelect = new SqlCommand("SELECT Vehiculo.patente AS patente, Vehiculo.marca AS marca, Modelo.precio AS precio  FROM Vehiculo, Modelo WHERE Vehiculo.estado = 1 AND Vehiculo.modelo_nombre = Modelo.nombre", cnn);
                            dr = cmdSelect.ExecuteReader();
                            while (dr.Read())
                            {
                                Console.WriteLine("Vehículos");
                                Console.WriteLine(count2 + 1 + ") Patente: " + dr["patente"].ToString() + " Marca: " + dr["marca"].ToString());
                                vehiculo[count2, 0] = dr["patente"].ToString();
                                vehiculo[count2, 1] = Int32.Parse(dr["precio"].ToString());
                                count2++;
                            }
                            dr.Close();
                            Console.Write("Seleccione el número del vehículo : ");
                            string nVehiculo = Console.ReadLine();

                            if (Int32.Parse(nVehiculo) <= count2) {
                                Console.Write("Seleccione cantidad de dias de arriendo : ");
                                string nArriendo = Console.ReadLine();
                                int dias = Int32.Parse(nArriendo);
                                int selecVehiculo = Int32.Parse(nVehiculo);
                                DateTime fecha_inicio = DateTime.Now;
                                DateTime fecha_devolucion = fecha_inicio.AddDays(dias);
                                int precio_total = (int)vehiculo[selecVehiculo, 1];
                                Console.Write("Precio Total: " + precio_total);
                                SqlCommand cmdInsert = new SqlCommand("Insert INTO arriendo (fecha_inicio, dias, total_arriendo, fecha_devolucion, cliente_rut, vehiculo_patente, usuario_nombre) VALUES(convert(datetime, '" + fecha_inicio.ToString() + "', 103),'" + dias + "','" + dias * precio_total + "',convert(datetime, '" + fecha_devolucion.ToString() + "', 103) ,'" + cliente[Int32.Parse(nCliente)] + "' ,'" + vehiculo[selecVehiculo, 0] + "' ,'" + usuario[0].ToString() + "') ", cnn) ;
                                cmdInsert.ExecuteNonQuery();
                                //SqlCommand cmdUpdate = new SqlCommand("Update Vehiculo set estado = 0 where patente =  " + vehiculo[selecVehiculo, 0].ToString(), cnn);
                                //cmdUpdate.ExecuteNonQuery();
                                SqlCommand cmdUpdateParametros = new SqlCommand("Update Vehiculo set estado = 0 where patente = @patente", cnn);
                                cmdUpdateParametros.Parameters.Add("patente", System.Data.SqlDbType.VarChar).Value = vehiculo[selecVehiculo, 0];
                                cmdUpdateParametros.ExecuteNonQuery();
                                Console.WriteLine(" Arriendo realizado correctamente para el cliente con RUT N°: " + cliente[Int32.Parse(nCliente)] + " Vehiculo patente: " + vehiculo[selecVehiculo, 0]);
                            } else {
                                Console.Write("Numero vehiculo no válido");
                            }
                        } else {
                            Console.Write("Numero cliente no válido");
                        }
                        
                    } else {
                        Console.WriteLine("Sin clientes dentro del sistema");
                    }   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la conexión " + ex.ToString());
            }
            finally

            {
                cmdSelect.Dispose();
                dr.Close();

            }
        }

        private static void ActualizacionDias(Object[] usuario) {

            string cnnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\camil\source\repos\PruebaDialogo\PruebaDialogo\BaseDatos.mdf;Integrated Security=True";

            SqlCommand cmdSelect = null;
            SqlDataReader dr = null;
            int count = 0;
            Object[,] arriendo = new Object[50, 2];

            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnStr))
                {
                    cnn.Open();

                   
                    cmdSelect = new SqlCommand("SELECT id_arriendo, fecha_inicio, dias, total_arriendo, fecha_devolucion, cliente_rut, vehiculo_patente, usuario_nombre FROM Arriendo", cnn);

                    dr = cmdSelect.ExecuteReader();

                    //Asignación de valores al objeto bajo referencia de memoria
                    while (dr.Read()) {
                        Console.WriteLine("Arriendo");
                        Console.WriteLine(count +1 + ") ID ARRIENDO: " + dr["id_arriendo"].ToString() + " FECHA INICIO: " + dr["fecha_inicio"].ToString() + " DIAS: " + dr["dias"].ToString() + " TOTAL ARRIENDO: " + dr["total_arriendo"].ToString() + " FECHA DEVOLUCION: " + dr["fecha_devolucion"].ToString() + " RUT CLIENTE: " + dr["cliente_rut"].ToString() + " PATENTE VEHICULO: " + dr["vehiculo_patente"].ToString() + " NOMBRE USUARIO: " + dr["usuario_nombre"].ToString());
                        arriendo[count, 0] = Int32.Parse(dr["id_arriendo"].ToString());
                        arriendo[count, 1] = Int32.Parse(dr["dias"].ToString());
                        count++;
                    }
                    dr.Close();

                    Console.Write("Seleccionar Arriendo: ");
                    string nArriendo = Console.ReadLine();
                    int selectArriendo = Int32.Parse(nArriendo);
                    Console.Write("Ingresar nueva cantidad de días: ");
                    string nDias = Console.ReadLine();
                    int selectDias = Int32.Parse(nDias);
                    int suma = (int)arriendo[selectArriendo, 1] + selectDias;

                    if (Int32.Parse(nArriendo) <= count && selectDias > 0) {
                        SqlCommand cmdUpdateParametros = new SqlCommand("Update Arriendo set dias = @dias where id_arriendo = @id_arriendo", cnn);
                        cmdUpdateParametros.Parameters.Add("dias", System.Data.SqlDbType.Int).Value = (int)arriendo[selectArriendo, 1] + selectDias;
                        cmdUpdateParametros.Parameters.Add("id_arriendo", System.Data.SqlDbType.Int).Value = arriendo[selectArriendo, 0];
                        cmdUpdateParametros.ExecuteNonQuery();
                        Console.WriteLine("Dias anteriores " + (int)arriendo[selectArriendo, 1] );
                        Console.WriteLine("Dias a agregar " + selectDias);
                        Console.WriteLine("Dias totales " + suma);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error en la conexión " + ex.ToString());
            }
            finally {
                cmdSelect.Dispose();
                dr.Close();

            }
        }

        

    }
}
