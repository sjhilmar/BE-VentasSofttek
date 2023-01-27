using BE_VentasSofttek.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BE_VentasSofttek.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository repository;

            public UsuarioController(IUsuarioRepository repository)
        {
            this.repository = repository;   
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await repository.ListarUsuarios();
            return Ok(lista);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Usuario(int id)
        {
            var venta = await repository.Usuario(id);
            if (venta == null) return NotFound();
            return Ok(venta);

        }
    }
}
