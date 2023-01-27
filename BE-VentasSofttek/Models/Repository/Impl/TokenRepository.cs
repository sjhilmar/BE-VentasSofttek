using BE_VentasSofttek.Data;
using BE_VentasSofttek.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_VentasSofttek.Models.Repository.Impl
{
    public class TokenRepository : ITokenRepository
    {

        private readonly VentasBDContext context;
        public IConfiguration configuration;
        public TokenRepository(VentasBDContext context, IConfiguration configuration)
        {
            this.context = context; 
            this.configuration = configuration; 
        }
        public TokenDTO GenerarToken([FromBody] Object optData)
        {
            
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());
            string _usuario = data.usuario;
            string _contrasenia = data.contrasenia;

            var usuario = context.Usuario.Where(x => x.usuario == _usuario && x.contrasenia==_contrasenia).FirstOrDefault();
            //var asesor = context.Usuario.Find(id);
            if (usuario == null)
            {
               return new TokenDTO { success = false, message= "Credenciales incorrectas",result="" };
            }
            var jwt = configuration.GetSection("Jwt").Get<JwtDTO>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim ("Id",usuario.Id.ToString()),
                new Claim ("usuario",usuario.usuario),
                new Claim ("contrasenia",usuario.contrasenia)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signIn
                );

            return new TokenDTO { success = true, message = "Exito", result = new JwtSecurityTokenHandler().WriteToken(token) };
            
        }

        public TokenDTO validarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new TokenDTO { success = false, message = "Verificar si estas enviando un token valido", result = "" };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "Id").Value;
                var usuario = context.Usuario.Find(Convert.ToInt32(id));
                return new TokenDTO { success = true, message = "Exito", result = usuario.ToString() };
                //return new TokenDTO { success = true, message = "Exito", result = "" };

            }
            catch (Exception e)
            {
                return new TokenDTO { success = false, message = "Catch: " + e.Message, result = "" };
            }
        }
    }
}
