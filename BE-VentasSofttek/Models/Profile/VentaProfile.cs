using AutoMapper;
namespace BE_VentasSofttek.Models
{
    public class VentaProfile :Profile
    {
     public VentaProfile()
        {
            CreateMap<Venta, VentaDTO>();
            CreateMap<VentaDTO, Venta>();
        }

    }
}
