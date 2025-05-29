using ApiLafise.Models;
using ApiLafise.Repositories;
using Xunit;

namespace ApiLafise.Tests
{
    //Prueba unitaria para la creacion de cuentas bancarias
    public class CuentaTests
    {
        [Fact]
        public void InsertarCuenta_DeberiaRetornarTrue()
        {
            string numeroCuentaUnico = "TEST-" + Guid.NewGuid().ToString("N").Substring(0, 8);
            var cuentaRepo = new CuentaRepository();
            cuentaRepo.InsertarCuenta(new CuentaBancaria
            {
                NumeroCuenta = numeroCuentaUnico,
                ClienteId = 1,
                Saldo = 10
            });

            var transaccion = new Transaccion
            {
                NumeroCuenta = numeroCuentaUnico, // Usar el mismo valor generado
                Tipo = "Retiro",
                Monto = 999999
            };

        }
    }
}
