using AutoMapper;
using BackendDemo.Domain;
using BackendDemo.Infrastructure;
using BackendDemo.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BackendDemo.Application.Productos.Commands
{
    public class UpdateProducto
    {
        public class UpdateProductoCommand : IRequest<UpdateProductoResponse>
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
        }

        public class UpdateProductoResponse
        {
            public Producto producto { get; set; }
        }
        public class CommandValidator : AbstractValidator<UpdateProductoCommand>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                    RuleFor(x => x.Nombre).NotEmpty();
                }
            }

        public class Handler : IRequestHandler<UpdateProductoCommand, UpdateProductoResponse>
        {
            private readonly BackendDemoContext context;
            private readonly IMapper mapper;

            public Handler(BackendDemoContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<UpdateProductoResponse> Handle(UpdateProductoCommand command, CancellationToken cancellationToken)
            {
                var producto = context.Productos.FirstOrDefault(x => x.Id == command.Id);
                
                if (producto is null)
                    throw new RestException(HttpStatusCode.NotFound, new { Producto = Constants.NOT_FOUND });

                mapper.Map(command, producto);

                await context.SaveChangesAsync(cancellationToken);
                return new UpdateProductoResponse { producto = producto };
            }
        }
    }
    
}
