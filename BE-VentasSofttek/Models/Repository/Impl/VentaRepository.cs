using BE_VentasSofttek.Data;
using Microsoft.EntityFrameworkCore;

namespace BE_VentasSofttek.Models.Repository.Impl
{
    public class VentaRepository : IVentaRepository
    {
        private readonly VentasBDContext context;

        
        public VentaRepository(VentasBDContext context)
        {
            this.context = context;
            List<Venta> venta = new List<Venta>();
            if(context .Venta.Count()==0) { 
                venta.Add(new Venta { fecha = DateTime.Now, cliente = "Primer cliente", vendedor = "Asesor1", producto = "LapTop", cantidad = 1, precio = 1500, importe = 1500 });
                venta.Add(new Venta { fecha = DateTime.Now, cliente = "Segundo cliente", vendedor = "Asesor1", producto = "Servidor", cantidad = 1, precio = 8500, importe = 8500 });
                venta.Add(new Venta { fecha = DateTime.Now, cliente = "Tercer cliente", vendedor = "Asesor1", producto = "Monitor", cantidad = 1, precio = 750, importe = 750 });
                context.Venta.AddRange(venta);
                context.SaveChanges();
            }
        }

        public async Task<List<Venta>> ListarVentas()
        {
            return await context.Venta.ToListAsync();
        }

        public async Task<Venta> venta(int id)
        {
            return await context.Venta.FindAsync(id);
        }

        public async Task<Venta> guardar(Venta venta)
        {
            context.Venta.Add(venta);
            await context.SaveChangesAsync();
            return venta;
        }
        public async Task<Venta> actualizar(Venta venta, int id)
        {
            var ventaItem  = await context.Venta.FirstOrDefaultAsync(x => x.Id == id);  
            if (ventaItem != null)
            {
                //context.Entry(venta).State = EntityState.Modified;  
                ventaItem.cliente = venta.cliente;
                ventaItem.vendedor = venta.vendedor;
                ventaItem.producto = venta.producto;
                ventaItem.cantidad = venta.cantidad;
                ventaItem.precio = venta.precio;
                ventaItem.importe = venta.importe;

                await context.SaveChangesAsync();
            }
            return venta;
        }

        public async Task<bool> eliminar(int id)
        {
            var venta = await context.Venta.FindAsync(id);
            if (venta == null) return false;
            context.Venta.Remove(venta);
            await context.SaveChangesAsync();   
            return true;
        }

       

      

        
    }
}
