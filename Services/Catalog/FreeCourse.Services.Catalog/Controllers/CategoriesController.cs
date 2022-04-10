using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCategory.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService<CategoryDto> _categoryService;

        public CategoriesController(ICategoryService<CategoryDto> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return CreateActionResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return CreateActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var response = await _categoryService.CreateAsync(categoryDto);
            return CreateActionResult(response);
        }


    }
}
