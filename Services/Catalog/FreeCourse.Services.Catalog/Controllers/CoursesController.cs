using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : BaseController
    {

        private readonly ICourseService<CourseDto> _courseService;

        public CoursesController(ICourseService<CourseDto> courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return CreateActionResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            return CreateActionResult(response);
        }

        [Route("/api/[controller]/GetAllByUserId/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var response = await _courseService.GetByUserIdAsync(userId);
            return CreateActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseDto courseDto)
        {
            var response = await _courseService.CreateAsync(courseDto);
            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseDto courseDto)
        {
            var response = await _courseService.UpdateAsync(courseDto);
            return CreateActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteAsync(id);
            return CreateActionResult(response);
        }
    }
}
