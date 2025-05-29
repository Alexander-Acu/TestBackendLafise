using ApiLafise.Models;
using System.Data.SQLite;

namespace ApiLafise.Repositories
{
    //Metodo que inserta o crea una cuenta bancaria en la base de datos
    public class CuentaRepository
    {
        private readonly string _connectionString = "Data Source=BaseDatos/lafiseBD.db;Version=3;";
        public bool InsertarCuenta(CuentaBancaria cuenta)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var sql = "INSERT INTO CuentasBancarias (ClienteId, NumeroCuenta, SaldoInicial) VALUES (@ClienteId, @NumeroCuenta, @SaldoInicial)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ClienteId", cuenta.ClienteId);
                    cmd.Parameters.AddWithValue("@NumeroCuenta", cuenta.NumeroCuenta);
                    cmd.Parameters.AddWithValue("@SaldoInicial", cuenta.SaldoInicial);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
        }


        //Metodo encargado de obtener el saldo pasando como parametro el numero de cuenta 
        public double? ObtenerSaldoPorNumeroCuenta(string numeroCuenta)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var sql = "SELECT SaldoInicial FROM CuentasBancarias WHERE NumeroCuenta = @NumeroCuenta";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NumeroCuenta", numeroCuenta);
                    var resultado = cmd.ExecuteScalar();

                    if (resultado != null && double.TryParse(resultado.ToString(), out double saldo))
                    {
                        return saldo;
                    }
                    return null;
                }
            }
        }

    }
}
