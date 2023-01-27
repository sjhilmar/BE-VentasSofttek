namespace BE_VentasSofttek.Models.Repository
{
    public interface IVentaRepository
    {
        Task<List<Venta>> ListarVentas();
        Task<Venta> venta(int id);
        Task<bool> eliminar(int id);  
        Task<Venta> guardar(Venta venta);
        Task<Venta> actualizar(Venta venta, int id);
    }
}
