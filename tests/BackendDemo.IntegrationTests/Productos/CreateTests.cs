using BackendDemo.Application.Productos.Commands;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static BackendDemo.Application.Productos.Commands.CreateProducto;

namespace BackendDemo.IntegrationTests.Features.Articles
{
    public class CreateTests : SliceFixture
    {
        [Fact]
        public async Task Expect_Create_Producto()
        {
            var context = GetDbContext();
            var mapper = GetMapper();

            var command = new CreateProductoCommand()
            {
                Nombre = "Nuevo Producto test",
                Descripcion = "descripcion producto test"
            };

            var handler = new CreateProducto.Handler(context, mapper);
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            var dbResult = context.Productos.FirstOrDefault(x => x.Id == result.producto.Id);
            
            Assert.NotNull(result);
            Assert.Equal(result.producto.Nombre, command.Nombre);
            Assert.Equal(result.producto.Descripcion, command.Descripcion);
            Assert.Equal(dbResult.Nombre, command.Nombre);
            Assert.Equal(dbResult.Descripcion, command.Descripcion);
        }
    }
}