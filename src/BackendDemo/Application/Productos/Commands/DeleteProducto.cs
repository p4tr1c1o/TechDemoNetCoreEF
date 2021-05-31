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
    public class DeleteProducto
    {
        public class DeleteProductoCommand : IRequest<Unit>
        {
            public int Id { get; set; }
        }

        

        public class CommandValidator : AbstractValidator<DeleteProductoCommand>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }

        public class Handler : IRequestHandler<DeleteProductoCommand, Unit>
        {
            private readonly BackendDemoContext context;

            public Handler(BackendDemoContext context)
            {
                this.context = context;

            }

            public async Task<Unit> Handle(DeleteProductoCommand command, CancellationToken cancellationToken)
            {
                var producto = context.Productos.FirstOrDefault(x => x.Id == command.Id);
                
                if (producto is null)
                    throw new RestException(HttpStatusCode.NotFound, new { Producto = Constants.NOT_FOUND });

                context.Productos.Remove(producto);

                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
    
}
