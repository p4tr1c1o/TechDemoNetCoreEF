using BackendDemo.Domain;
using BackendDemo.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendDemo.Application.Productos.Queries
{
    public class GetProductosQuery : IRequest<GetProductosResponse> { }

    public class GetProductosResponse
    {
        public List<Producto> Productos { get; set; }
    }

    public class GetProductos
    {
        public class CommandValidator : AbstractValidator<GetProductosQuery>
        {
            public CommandValidator() { }
        }

        public class Handler : IRequestHandler<GetProductosQuery, GetProductosResponse>
        {
            private readonly BackendDemoContext context;

            public Handler(BackendDemoContext context)
            {
                this.context = context;
            }

            public async Task<GetProductosResponse> Handle(GetProductosQuery query, CancellationToken cancellationToken)
            {
                var result = context.Productos.AsNoTracking().ToList();

                await context.SaveChangesAsync();

                return new GetProductosResponse
                {
                    Productos = result
                };
            }
        }
    }
}
