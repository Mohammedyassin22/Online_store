using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresentions
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceProduct serviceProduct):ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result =await serviceProduct.AuthService.LoginAsync(loginDto);
            return Ok(result);
        }
        [HttpPost("rgister")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await serviceProduct.AuthService.RegisterDto(registerDto);
            return Ok(result);
        }
    }
}
