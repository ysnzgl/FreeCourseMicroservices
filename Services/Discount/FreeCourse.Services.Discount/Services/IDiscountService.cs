using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<ResponseDto<List<EntDiscount>>> GetAll();

        Task<ResponseDto<EntDiscount>> GetById(int id);

        Task<ResponseDto<NoContent>> Save(EntDiscount discount);

        Task<ResponseDto<NoContent>> Update(EntDiscount discount);

        Task<ResponseDto<NoContent>> Delete(int id);

        Task<ResponseDto<EntDiscount>> GetByCodeAndUserId(string code, string userId);



    }
}
