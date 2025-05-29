using ApiLafise.Models;
using ApiLafise.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiLafise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionController : ControllerBase
    {
        private readonly TransaccionRepository _repo;

        public TransaccionController()
        {
            _repo = new TransaccionRepository();
        }

        [HttpPost]
        public IActionResult Registrar([FromBody] Transaccion transaccion)
        {
            var resultado = _repo.RegistrarTransaccion(transaccion);
            if (resultado == "Transacción realizada con éxito.")
                return Ok(resultado);
            else
                return BadRequest(resultado);
        }

        [HttpGet("historial/{numeroCuenta}")]
        public IActionResult Historial(string numeroCuenta)
        {
            var transacciones = _repo.ObtenerHistorial(numeroCuenta);
            if (transacciones.Any())
            {
                var saldoFinal = transacciones.Last().SaldoDespues;
                return Ok(new
                {
                    NumeroCuenta = numeroCuenta,
                    Transacciones = transacciones,
                    SaldoFinal = saldoFinal
                });
            }
            else
            {
                return NotFound("No hay transacciones para esta cuenta." );
            }
        }

    }
}
