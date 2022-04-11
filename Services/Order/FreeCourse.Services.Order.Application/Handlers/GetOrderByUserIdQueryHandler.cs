using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Application.Mappings;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Handlers
{
    public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrderByUserIdQuery, ResponseDto<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrderByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();
            List<OrderDto> result = new List<OrderDto>();
            if (orders.Any())
            {
                result = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            }
            return ResponseDto<List<OrderDto>>.Success(result, 200);
        }
    }
}
