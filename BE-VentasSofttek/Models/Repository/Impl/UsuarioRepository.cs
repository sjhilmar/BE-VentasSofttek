using BE_VentasSofttek.Data;
using Microsoft.EntityFrameworkCore;

namespace BE_VentasSofttek.Models.Repository.Impl
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly VentasBDContext context;
        public UsuarioRepository(VentasBDContext context)
        {
            this.context = context;
            if (context.Usuario.Count() == 0)
            {
                List<Usuario> asesor = new List<Usuario>();
                asesor.Add(new Usuario { usuario = "Asesor1", contrasenia="abcd1234" });
                context.Usuario.AddRange(asesor);
                context.SaveChanges();
            }
        }
        public async Task<List<Usuario>> ListarUsuarios()
        {
            return await context.Usuario.ToListAsync();

        }

        public async Task<Usuario> Usuario(int id)
        {
            return await context.Usuario.FindAsync(id);
        }
    }
}
