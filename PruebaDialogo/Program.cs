using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PruebaDialogo
{
    class Program
    {
        static void Main(string[] args){

            string connString = @"Server =.\SQL2K17; Database = SampleDB; Trusted_Connection = True;";
            int empID;
            string empCode, empFirstName, empLastName, locationCode, locationDescr;

            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(connString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"SELECT e.id,e.code,e.firstName,e.lastName,l.code,l.descr
                                     FROM employees e
                                     INNER JOIN location l on e.locationID=l.id;
                                     ";
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    Console.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                    Console.WriteLine("Retrieved records:");

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            empID = dr.GetInt32(0);
                            empCode = dr.GetString(1);
                            empFirstName = dr.GetString(2);
                            empLastName = dr.GetString(3);
                            locationCode = dr.GetString(4);
                            locationDescr = dr.GetString(5);

                            //display retrieved record
                            Console.WriteLine("{0},{1},{2},{3},{4},{5}", empID.ToString(), empCode, empFirstName, empLastName, locationCode, locationDescr);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }

            char op = '1';
            string op2 = "k";
            string posicion;
            string nombre;

            do
            {
                Console.Clear();
                Console.WriteLine("Inicia Sesión");
                string username, password = string.Empty;

                Console.WriteLine("Ingresa tu nombre de usuario");
                username = Console.ReadLine();

                Console.WriteLine("Ingresa tu contraseña");
                password = Console.ReadLine();



                op = Console.ReadKey(true).KeyChar;

                switch (op)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Agregar Posicion");
                        posicion = Console.ReadLine();
                        Console.WriteLine("Agregar nombre");
                        nombre = Console.ReadLine();
                        op2 = "k";
                        break;

                    case '2':
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("Presione cualquier tecla");
                        Console.ReadKey();
                        op2 = "k";
                        break;

                    case '3':
                        op2 = "s";
                        break;

                }


            } while (op2 == "k");
        }
    }
}
