using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<ResponseDto<bool>> CreateOrUpdateAsync(BasketDto basketDto)
        {
            var basketString = JsonSerializer.Serialize(basketDto);
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, basketString);
            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket could not create or update", 500);

        }

        public async Task<ResponseDto<bool>> DeleteAsync(string userId)
        {
            await _redisService.GetDb().KeyDeleteAsync(userId);

            return ResponseDto<bool>.Success(204);
        }

        public async Task<ResponseDto<BasketDto>> GetBasketByUserAsync(string userId)
        {
            var basketString = await _redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(basketString))
                return ResponseDto<BasketDto>.Fail("Basket Not Found", 404);

            BasketDto basket = JsonSerializer.Deserialize<BasketDto>(basketString);
            return ResponseDto<BasketDto>.Success(basket, 200);

        }
    }
}
