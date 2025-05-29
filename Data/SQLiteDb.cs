using System.Data.SQLite;

public class SQLiteDb
{
    public static class SQLitedab
    {
        private static readonly string connectionString;

        static SQLitedab()
        {
            string dbPath = Path.Combine(AppContext.BaseDirectory, "BaseDatos", "lafiseBD.db");

            if (!File.Exists(dbPath))
            {
                throw new FileNotFoundException($"No se encontró la base de datos en la ruta: {dbPath}");
            }

            connectionString = $"Data Source={dbPath};Version=3;";
        }


        public static SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
