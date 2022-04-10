using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICategoryService<T> where T : CategoryDto
    {
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();

        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto);

        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);
    }
}
