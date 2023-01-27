using AutoMapper;
using BE_VentasSofttek.Models;
using BE_VentasSofttek.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_VentasSofttek.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly IVentaRepository repository;
        private readonly ITokenRepository tokenRepository;
        private readonly IMapper mapper;


        public VentaController(IVentaRepository repository, IMapper mapper, ITokenRepository tokenRepository)
        {
            this.repository = repository;
            this.tokenRepository = tokenRepository;
            this.mapper = mapper;   
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Listar()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = tokenRepository.validarToken(identity);

            if (rToken.success)
            {
                var lista = await repository.ListarVentas();
                var listaDTO = mapper.Map<List<VentaDTO>>(lista);
                return Ok(listaDTO);
            }
            else
            {
                return NotFound();  
            }
            
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Venta(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = tokenRepository.validarToken(identity);

            if (rToken.success)
            {
                var venta = await repository.venta(id);
                if (venta == null) return NotFound();
                var ventaDTO = mapper.Map<VentaDTO>(venta);
                return Ok(venta);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> guardar(VentaDTO ventaDTO)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = tokenRepository.validarToken(identity);

            if (rToken.success)
            {
                var venta = mapper.Map<Venta>(ventaDTO);
                venta.fecha = DateTime.Now;
                await repository.guardar(venta);
                var ventaItemDto = mapper.Map<VentaDTO>(venta);
                return CreatedAtAction(nameof(Venta), new { id = ventaItemDto.Id }, ventaItemDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult>actualizar(VentaDTO ventaDTO, int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = tokenRepository.validarToken(identity);

            if (rToken.success)
            {
                var venta = mapper.Map<Venta>(ventaDTO);
                venta.fecha= DateTime.Now;

                if (venta.Id!=id) return BadRequest();
                var _venta = await repository.actualizar(venta, id);
                return Ok(venta);
            }
            return BadRequest();
        }
        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult>Eliminar(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = tokenRepository.validarToken(identity);

            if (rToken.success)
            {
                var ventaItem = repository.venta(id);
                if (ventaItem==null)return NotFound();
                await repository.eliminar(id);
                return NoContent();
            }
            return BadRequest();
        }

    }
}
