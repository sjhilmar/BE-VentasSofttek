namespace BE_VentasSofttek.Models.Repository
{
    public interface IUsuarioRepository
    {

        Task<List<Usuario>> ListarUsuarios();
        Task<Usuario> Usuario(int id);
    }
}
