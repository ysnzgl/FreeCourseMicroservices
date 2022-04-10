using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public class CourseService : ICourseService<CourseDto>
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var db = client.GetDatabase(databaseSettings.DBName);

            _courseCollection = db.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = db.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(c => true).ToListAsync();
            List<CourseDto> courseDtos = new List<CourseDto>();
            if (courses.Any())
            {

                foreach (var item in courses)
                {
                    var category = await _categoryCollection.Find(c => c.Id == item.CategoryId).FirstOrDefaultAsync();
                    item.Category = category;
                }
                courseDtos = _mapper.Map<List<CourseDto>>(courses);
            }
            return ResponseDto<List<CourseDto>>.Success(courseDtos, 200);
        }

        public async Task<ResponseDto<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return ResponseDto<CourseDto>.Fail("Not Found", 404);

            }
            course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
            var courseDto = _mapper.Map<CourseDto>(course);
            return ResponseDto<CourseDto>.Success(courseDto, 200);
        }

        public async Task<ResponseDto<List<CourseDto>>> GetByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();
            List<CourseDto> courseDtos = new List<CourseDto>();
            if (courses.Any())
            {

                foreach (var item in courses)
                {
                    var category = await _categoryCollection.Find(c => c.Id == item.CategoryId).FirstOrDefaultAsync();
                    item.Category = category;
                }
                courseDtos = _mapper.Map<List<CourseDto>>(courses);
            }
            return ResponseDto<List<CourseDto>>.Success(courseDtos, 200);
        }

        public async Task<ResponseDto<CourseDto>> CreateAsync(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            await _courseCollection.InsertOneAsync(course);
            courseDto.Id = course.Id;
            return ResponseDto<CourseDto>.Success(courseDto, 200);
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(c => c.Id == course.Id, course);
            if (result == null)
            {
                return ResponseDto<NoContent>.Fail("Not Found", 404);
            }
            return ResponseDto<NoContent>.Success(204);
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(c => c.Id == id);
            if (result.DeletedCount <= 0)
            {
                return ResponseDto<NoContent>.Fail("Not Found", 404);
            }
            return ResponseDto<NoContent>.Success(204);
        }

       
    }


}
