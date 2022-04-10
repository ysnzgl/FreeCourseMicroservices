using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourseService<T> where T : CourseDto
    {
        Task<ResponseDto<List<CourseDto>>> GetAllAsync();

        Task<ResponseDto<CourseDto>> CreateAsync(CourseDto CourseDto);

        Task<ResponseDto<CourseDto>> GetByIdAsync(string id);
        Task<ResponseDto<NoContent>> UpdateAsync(CourseDto courseDto);
        Task<ResponseDto<NoContent>> DeleteAsync(string id);

        Task<ResponseDto<List<CourseDto>>> GetByUserIdAsync(string userId);
    }
}
