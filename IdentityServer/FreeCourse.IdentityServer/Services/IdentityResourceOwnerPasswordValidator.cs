using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            ApplicationUser existUser = context.UserName.IndexOf("@") > -1 ? await _userManager.FindByEmailAsync(context.UserName) :
                await _userManager.FindByNameAsync(context.UserName);

            if (existUser != null)
            {
                var passWordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
                if (passWordCheck)
                {
                    context.Result = new GrantValidationResult(existUser.Id, OidcConstants.AuthenticationMethods.Password);
                    return;
                }

            }


            var errors = new Dictionary<string, object>();
            errors.Add("errors", new List<string> { "warning email or password" });
            context.Result.CustomResponse = errors;
            return;

        }


    }
}
