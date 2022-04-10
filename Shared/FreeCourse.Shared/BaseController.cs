using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCourse.Shared
{
    public class BaseController : ControllerBase
    {
        public IActionResult CreateActionResult<T>(ResponseDto<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
