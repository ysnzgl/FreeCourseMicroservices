using FreeCourse.Services.Discount.Models;
using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : BaseController
    {
        ISharedIdentityService _identityService;
        IDiscountService _discountService;

        public DiscountsController(ISharedIdentityService identityService, IDiscountService discountService)
        {
            _identityService = identityService;
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _discountService.GetAll();
            return CreateActionResult(discounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var discounts = await _discountService.GetById(id);
            return CreateActionResult(discounts);
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _identityService.GetUserId;
            var discounts = await _discountService.GetByCodeAndUserId(code, userId);
            return CreateActionResult(discounts);
        }

        [HttpPost]
        public async Task<IActionResult> Save(EntDiscount discount)
        {
            discount.UserId = _identityService.GetUserId;
            var state = await _discountService.Save(discount);
            return CreateActionResult(state);
        }

        [HttpPut]
        public async Task<IActionResult> Update(EntDiscount discount)
        {
            var state = await _discountService.Update(discount);
            return CreateActionResult(state);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var state = await _discountService.Delete(id);
            return CreateActionResult(state);
        }


    }
}
