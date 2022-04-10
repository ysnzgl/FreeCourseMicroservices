using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var basket = await _basketService.GetBasketByUserAsync(_sharedIdentityService.GetUserId);
            return CreateActionResult(basket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(BasketDto basket)
        {
            var state=await _basketService.CreateOrUpdateAsync(basket);
            return CreateActionResult(state);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var state = await _basketService.DeleteAsync(_sharedIdentityService.GetUserId);
            return CreateActionResult(state);
        }
    }
}
