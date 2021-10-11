using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA211011_2
{
    class Program
    {
        static void Main()
        {
            string connectionString =
                @"Server=(localdb)\MSSQLLocalDB;" +
                "Database=teszt;";

            var connection = new SqlConnection(connectionString);
            connection.Open();

            var sqlCommand = new SqlCommand("SELECT * FROM autok;", connection);

            var sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Console.WriteLine(
                    $"{sqlDataReader[0]} " +
                    $"{sqlDataReader["marka"]} " +
                    $"{sqlDataReader[2]} " +
                    $"{sqlDataReader.GetInt32(3) + 10} ");
            }

            sqlDataReader.Close();

            Console.Write("új márka: ");
            string m = Console.ReadLine();
            Console.Write("új típus: ");
            string t = Console.ReadLine();
            Console.Write("új végsebesség: ");
            string v = Console.ReadLine();

            sqlCommand = new SqlCommand(
                $"INSERT INTO autok VALUES ('{m}', '{t}', {v});",
                connection);

            var sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.InsertCommand = sqlCommand;
            sqlDataAdapter.InsertCommand.ExecuteNonQuery();

            Console.WriteLine("done!");

            connection.Close();
            Console.ReadKey();
        }
    }
}
