using AutoMapper;
using BackendDemo.Domain;
using BackendDemo.Infrastructure;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendDemo.Application.Productos.Commands
{
    public class CreateProducto
    {
        public class CreateProductoCommand : IRequest<CreateProductoResponse>
        {
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
        }

        public class CreateProductoResponse
        {
            public Producto producto { get; set; }
        }
        public class CommandValidator : AbstractValidator<CreateProductoCommand>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Nombre).NotEmpty();
                }
            }

        public class Handler : IRequestHandler<CreateProductoCommand, CreateProductoResponse>
        {
            private readonly BackendDemoContext context;
            private readonly IMapper mapper;

            public Handler(BackendDemoContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateProductoResponse> Handle(CreateProductoCommand command, CancellationToken cancellationToken)
            {
                var nuevoProducto = mapper.Map<Producto>(command);

                context.Productos.Add(nuevoProducto);

                await context.SaveChangesAsync(cancellationToken);
                return new CreateProductoResponse { producto = nuevoProducto };
            }
        }
    }
    
}
