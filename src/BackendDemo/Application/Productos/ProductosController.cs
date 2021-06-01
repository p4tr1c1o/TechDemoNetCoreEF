using BackendDemo.Application.Productos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BackendDemo.Application.Productos.Commands.CreateProducto;
using static BackendDemo.Application.Productos.Commands.DeleteProducto;
using static BackendDemo.Application.Productos.Commands.UpdateProducto;

namespace BackendDemo.Application.Productos
{
    [Route("Productos")]
    public class ProductosController : Controller
    {
        private readonly IMediator mediator;

        public ProductosController(IMediator Mediator)
        {
            this.mediator = Mediator;
        }

        [HttpGet]
        public async Task<object> GetProductos()
        {
            var response = await mediator.Send(new GetProductosQuery());
            return response.Productos;
        }


        [HttpPost]
        public async Task<CreateProductoResponse> CreateProducto ([FromBody] CreateProductoCommand command)
        {
            var response = await mediator.Send(command);
            return response;
        }

        [HttpPut]
        public async Task<UpdateProductoResponse> UpdateProducto([FromBody] UpdateProductoCommand command)
        {
            var response = await mediator.Send(command);
            return response;
        }

        [HttpDelete]
        public async Task<Unit> DeleteProducto([FromBody] DeleteProductoCommand command)
        {
            var response = await mediator.Send(command);
            return response;
        }

    }

}
