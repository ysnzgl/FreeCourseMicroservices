using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public class CategoryService : ICategoryService<CategoryDto>
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var db = client.GetDatabase(databaseSettings.DBName);

            _categoryCollection = db.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(c => true).ToListAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return ResponseDto<List<CategoryDto>>.Success(categoryDtos, 200);
        }

        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(category);
            categoryDto.Id = category.Id;
            return ResponseDto<CategoryDto>.Success(categoryDto, 200);
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return ResponseDto<CategoryDto>.Fail("Not Found", 404);

            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return ResponseDto<CategoryDto>.Success(categoryDto, 200);
        }

    }
}
