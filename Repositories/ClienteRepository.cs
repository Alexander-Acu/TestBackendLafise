using ApiLafise.Models;
using System.Data.SQLite;

namespace ApiLafise.Repositories
{

    //Metodo que inserta los clientes en la base de datos
    public class ClienteRepository
    {
        private readonly string _connectionString = "Data Source=BaseDatos/lafiseBD.db;Version=3;";
        
        public bool InsertarCliente(Cliente cliente)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var sql = "INSERT INTO Clientes (Nombre, FechaNacimiento, Sexo, Ingresos) VALUES (@Nombre, @FechaNacimiento, @Sexo, @Ingresos)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
                    cmd.Parameters.AddWithValue("@Ingresos", cliente.Ingresos);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
        }
    }
}
