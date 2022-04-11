using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Shared;
using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _identityService;

        public OrdersController(IMediator mediator, ISharedIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserId()
        {
            var response = await _mediator.Send(new GetOrderByUserIdQuery { UserId = _identityService.GetUserId });
            return CreateActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CreateOrderCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return CreateActionResult(response);
        }
    }
}
