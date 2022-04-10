using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace FreeCourse.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignupDto userDto)
        {
            var user = new ApplicationUser { UserName = userDto.UserName, City = userDto.City, Email = userDto.Email };
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
                return BadRequest(ResponseDto<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            return NoContent();
        }

        [HttpGet]

        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            return Ok(new { Id = user.Id, UserName = user.UserName, City = user.City, Email = user.Email });

        }
    }
}
