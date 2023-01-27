using BE_VentasSofttek.Models;
using BE_VentasSofttek.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BE_VentasSofttek.Controllers
{
    [ApiController]
    [Route("api/Token")]
    public class GenerarTokenController : ControllerBase
    {
        private readonly ITokenRepository repository;


        public GenerarTokenController(ITokenRepository repository)
        {
            this.repository = repository;   
        }


        [HttpPost]
        public dynamic GenerarToken([FromBody] Object optData)
        {
            var token = repository.GenerarToken(optData);
            return token;
        }
    }
}
