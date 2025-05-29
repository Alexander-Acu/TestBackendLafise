using ApiLafise.Models;
using ApiLafise.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiLafise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentaController : ControllerBase
    {
        private readonly CuentaRepository _repo;

        public CuentaController()
        {
            _repo = new CuentaRepository();
        }

        [HttpPost]
        public IActionResult CrearCuenta([FromBody] CuentaBancaria cuenta)
        {
            var resultado = _repo.InsertarCuenta(cuenta);
            if (resultado)
                return Ok("Cuenta bancaria creada correctamente.");
            else
                return BadRequest("Error al crear cuenta bancaria.");
        }

        [HttpGet("saldo/{numeroCuenta}")]
        public IActionResult ObtenerSaldo(string numeroCuenta)
        {
            var saldo = _repo.ObtenerSaldoPorNumeroCuenta(numeroCuenta);
            if (saldo != null)
                return Ok(new { NumeroCuenta = numeroCuenta, Saldo = saldo });
            else
                return NotFound("Cuenta no encontrada.");
        }
    }
}
