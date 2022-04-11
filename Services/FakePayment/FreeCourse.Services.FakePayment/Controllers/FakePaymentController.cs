using FreeCourse.Shared;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : BaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResult(ResponseDto<NoContent>.Success(200));
        }
    }
}
