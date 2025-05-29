using ApiLafise.Models;
using ApiLafise.Repositories;
using Xunit;

namespace ApiLafise.Tests
{
    public class TransaccionTests
    {
        private string GenerarNumeroCuentaUnico() =>
            "TEST-" + Guid.NewGuid().ToString("N").Substring(0, 8);

        [Fact]
        public void Deposito_DeberiaRetornarExito()
        {
            var cuentaRepo = new CuentaRepository();
            var numeroCuenta = GenerarNumeroCuentaUnico();
            cuentaRepo.InsertarCuenta(new CuentaBancaria
            {
                NumeroCuenta = numeroCuenta,
                ClienteId = 1,
                Saldo = 0
            });

            var repo = new TransaccionRepository();
            var transaccion = new Transaccion
            {
                NumeroCuenta = numeroCuenta,
                Tipo = "Deposito",
                Monto = 100
            };

            var resultado = repo.RegistrarTransaccion(transaccion);
            Assert.Equal("Transacción realizada con éxito.", resultado);
        }



        [Fact]
        public void Retiro_Valido_DeberiaRetornarExito()
        {
            var cuentaRepo = new CuentaRepository();
            var numeroCuenta = "TEST-" + Guid.NewGuid().ToString("N").Substring(0, 8);

            var insercionExitosa = cuentaRepo.InsertarCuenta(new CuentaBancaria
            {
                NumeroCuenta = numeroCuenta,
                ClienteId = 1,
                SaldoInicial = 100
            });

            Assert.True(insercionExitosa, "La cuenta no se insertó correctamente.");

            var repo = new TransaccionRepository();
            var transaccion = new Transaccion
            {
                NumeroCuenta = numeroCuenta,
                Tipo = "Retiro",
                Monto = 50
            };

            var resultado = repo.RegistrarTransaccion(transaccion);

            Assert.Equal("Transacción realizada con éxito.", resultado);
        }


        [Fact]
        public void Retiro_SinFondos_DeberiaRetornarError()
        {
            var cuentaRepo = new CuentaRepository();
            var numeroCuenta = GenerarNumeroCuentaUnico();
            cuentaRepo.InsertarCuenta(new CuentaBancaria
            {
                NumeroCuenta = numeroCuenta,
                ClienteId = 1,
                Saldo = 10
            });

            var repo = new TransaccionRepository();
            var transaccion = new Transaccion
            {
                NumeroCuenta = numeroCuenta,
                Tipo = "Retiro",
                Monto = 999999
            };

            var resultado = repo.RegistrarTransaccion(transaccion);
            Assert.Equal("Saldo insuficiente.", resultado);
        }

        [Fact]
        public void ObtenerHistorial_DeberiaRetornarTransacciones()
        {
            var cuentaRepo = new CuentaRepository();
            var numeroCuenta = GenerarNumeroCuentaUnico();
            cuentaRepo.InsertarCuenta(new CuentaBancaria
            {
                NumeroCuenta = numeroCuenta,
                ClienteId = 1,
                Saldo = 100
            });

            var repo = new TransaccionRepository();
            repo.RegistrarTransaccion(new Transaccion
            {
                NumeroCuenta = numeroCuenta,
                Tipo = "Deposito",
                Monto = 100
            });

            var historial = repo.ObtenerHistorial(numeroCuenta);

            Assert.NotNull(historial);
            Assert.True(historial.Count > 0);
        }
    }
}
