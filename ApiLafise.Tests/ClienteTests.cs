using ApiLafise.Models;
using ApiLafise.Repositories;
using Xunit;

namespace ApiLafise.Tests
{

    //Prueba unitaria para el segmento de Cliente
    public class ClienteTests
    {
        [Fact]
        public void InsertarCliente_DeberiaRetornarTrue()
        {
            var clienteRepo = new ClienteRepository();
            var cliente = new Cliente
            {
                Nombre = "Prueba_" + Guid.NewGuid().ToString("N").Substring(0, 8),
                FechaNacimiento = new DateTime(1990, 1, 1),
                Sexo = "M",
                Ingresos = 3000
            };
        
            var resultado = clienteRepo.InsertarCliente(cliente);
            Assert.True(resultado);
        }
    }
}
