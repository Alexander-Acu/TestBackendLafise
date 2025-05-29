using ApiLafise.Models;
using System.Data.SQLite;

namespace ApiLafise.Repositories
{
    public class TransaccionRepository
    {
        private readonly string _connectionString = "Data Source=BaseDatos/LafiseBD.db;Version=3;";

        public string RegistrarTransaccion(Transaccion transaccion)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                //Primero obtenemos el saldo actual  medianre el numero de cuenTA
                double saldoActual;
                using (var cmdSaldo = new SQLiteCommand("SELECT SaldoInicial FROM CuentasBancarias WHERE NumeroCuenta = @numCuenta", conn))
                {
                    cmdSaldo.Parameters.AddWithValue("@numCuenta", transaccion.NumeroCuenta);
                    var result = cmdSaldo.ExecuteScalar();
                    if (result == null)
                        return "Cuenta no encontrada.";

                    saldoActual = Convert.ToDouble(result);
                }

                //Calculamos el nuevo salgo en el caso en que el tipo de transaccion sea de tipo "Deposito"
                double nuevoSaldo = transaccion.Tipo == "Deposito"
                    ? saldoActual + transaccion.Monto
                    : saldoActual - transaccion.Monto;

                //Validamos que el retiro no sea mayor al saldo de la cuenta 
                if (transaccion.Tipo == "Retiro" && nuevoSaldo < 0)
                    return "Saldo insuficiente.";

                //aca validamos la parte de los Log's para dejar registro de todos los movimientos
                using (var transaction = conn.BeginTransaction())
                {
                    string insertSQL = @"INSERT INTO Transacciones (CuentaId, Tipo, Monto, SaldoDespues)
                                         VALUES (
                                            (SELECT Id FROM CuentasBancarias WHERE NumeroCuenta = @numCuenta),
                                            @Tipo, @Monto, @SaldoDespues
                                         );";

                    using (var cmd = new SQLiteCommand(insertSQL, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@numCuenta", transaccion.NumeroCuenta);
                        cmd.Parameters.AddWithValue("@Tipo", transaccion.Tipo);
                        cmd.Parameters.AddWithValue("@Monto", transaccion.Monto);
                        cmd.Parameters.AddWithValue("@SaldoDespues", nuevoSaldo);
                        cmd.ExecuteNonQuery();
                    }

                    //Actualizamps el saldo en cuenta
                    string updateSaldoSQL = "UPDATE CuentasBancarias SET SaldoInicial = @NuevoSaldo WHERE NumeroCuenta = @numCuenta";
                    using (var cmd = new SQLiteCommand(updateSaldoSQL, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@NuevoSaldo", nuevoSaldo);
                        cmd.Parameters.AddWithValue("@numCuenta", transaccion.NumeroCuenta);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }

                return "Transacción realizada con éxito.";
            }
        }


        //metodo para Calcular el historial de transacciones en la cuenta
        public List<Transaccion> ObtenerHistorial(string numeroCuenta)
        {
            var lista = new List<Transaccion>();

            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var sql = @"
                    SELECT T.Id, T.Tipo, T.Monto, T.SaldoDespues, T.FechaTransaccion
                    FROM Transacciones T
                    INNER JOIN CuentasBancarias C ON T.CuentaId = C.Id
                    WHERE C.NumeroCuenta = @numeroCuenta
                    ORDER BY T.FechaTransaccion ASC;
                ";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@numeroCuenta", numeroCuenta);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Transaccion
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NumeroCuenta = numeroCuenta,
                                Tipo = reader["Tipo"].ToString(),
                                Monto = Convert.ToDouble(reader["Monto"]),
                                SaldoDespues = Convert.ToDouble(reader["SaldoDespues"]),
                                FechaTransaccion = Convert.ToDateTime(reader["FechaTransaccion"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

    }
}

