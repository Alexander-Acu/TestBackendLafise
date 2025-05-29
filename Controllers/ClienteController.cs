using ApiLafise.Models;
using ApiLafise.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiLafise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository _repo;

        public ClienteController()
        {
            _repo = new ClienteRepository();
        }

        [HttpPost]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {
            var resultado = _repo.InsertarCliente(cliente);
            if (resultado)
                return Ok("Cliente registrado correctamente.");
            else
                return BadRequest("Error al registrar cliente.");
        }
    }
}
