using AutoMapper;
using BackendDemo.Domain;
using static BackendDemo.Application.Productos.Commands.CreateProducto;
using static BackendDemo.Application.Productos.Commands.UpdateProducto;

namespace BackendDemo.Application.Productos
{
   
    public class ProductosMapConfig : AutoMapper.Profile
    {
        public ProductosMapConfig()
        {
            CreateMap<CreateProductoCommand, Producto>(MemberList.Source);
            CreateMap<UpdateProductoCommand, Producto>(MemberList.Source);
        }
    }

}
