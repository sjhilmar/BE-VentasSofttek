using BE_VentasSofttek.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_VentasSofttek.Models.Repository
{
    public interface ITokenRepository
    {

        public TokenDTO GenerarToken([FromBody] Object optData);
        public TokenDTO validarToken(ClaimsIdentity identity);

    }
}
