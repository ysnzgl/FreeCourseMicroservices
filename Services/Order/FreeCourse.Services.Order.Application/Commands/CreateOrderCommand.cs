using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<ResponseDto<CreatedDto>>
    {
        public AddressDto Address { get; private set; }
        public string BuyerId { get; private set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
