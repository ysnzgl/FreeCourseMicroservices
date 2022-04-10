using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasketByUserAsync(string userId);

        Task<ResponseDto<bool>> CreateOrUpdateAsync(BasketDto basketDto);

        Task<ResponseDto<bool>> DeleteAsync(string id);
    }
}
