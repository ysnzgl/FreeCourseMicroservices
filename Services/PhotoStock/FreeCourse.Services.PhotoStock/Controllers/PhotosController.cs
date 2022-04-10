using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using var stream = new FileStream(path, FileMode.Create);

                await photo.CopyToAsync(stream, cToken);
                PhotoDto photoDto = new() { Url = $"photos/{photo.FileName}" };

                return CreateActionResult(ResponseDto<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResult(ResponseDto<NoContent>.Fail("Photo is Empty", 400));
        }

        [HttpDelete("{photoUrl}")]

        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            return CreateActionResult(ResponseDto<NoContent>.Success(204));
        }
    }
}
