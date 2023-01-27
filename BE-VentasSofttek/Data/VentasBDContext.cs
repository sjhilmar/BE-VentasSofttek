using BE_VentasSofttek.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_VentasSofttek.Data
{
    public class VentasBDContext : DbContext
    {

        public VentasBDContext(DbContextOptions<VentasBDContext> options) 
            : base(options)
        {

        }
        public DbSet<Venta> Venta { get; set; } 
        public DbSet<Usuario> Usuario { get; set; } 
    }
}
