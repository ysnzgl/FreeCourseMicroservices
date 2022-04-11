using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseDto<CreatedDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<CreatedDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAdress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.ZipCode, request.Address.Line);

            var entOrder = new Domain.OrderAggregate.Order(newAdress, request.BuyerId);
            foreach (var item in request.OrderItems)
            {
                entOrder.AddOrderItem(item.ProductId, item.ProductName, item.PictureUrl, item.Price);
            }

            await _context.Orders.AddAsync(entOrder);
            var result = await _context.SaveChangesAsync();

            var createdDto = new CreatedDto { Id = result, TypeOf = typeof(Domain.OrderAggregate.Order) };
            return ResponseDto<CreatedDto>.Success(createdDto, 200);

        }
    }
}
