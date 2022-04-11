using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Shared.Dtos;
using MediatR;
using System.Collections.Generic;

namespace FreeCourse.Services.Order.Application.Queries
{
    public class GetOrderByUserIdQuery : IRequest<ResponseDto<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
