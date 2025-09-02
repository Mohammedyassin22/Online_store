using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService(UserManager<AppUser> userManager,IOptions<Jwtoption>options) : IAuthService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user=await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnAuthorizedException();
            var flag=await userManager.CheckPasswordAsync(user,loginDto.Password);
            if(!flag) throw new UnAuthorizedException();
            return new UserResultDto()
            {
                DispalyName = user.DisplayName,
                Email=user.Email,
                Token= await GenerateTokenAsync(user)
            };
        }

        public async Task<UserResultDto> RegisterDto(RegisterDto registerDto)
        {
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber= registerDto.PhoneNumber
            };

            var result=await userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description);
                throw new ValidationException(errors);
            }
            
            return new UserResultDto()
            {
                DispalyName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };
        }

        private async Task<string>GenerateTokenAsync(AppUser user)
        {
            var jwtoptions = options.Value;
            var authclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                authclaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SecretKey));
            var token = new JwtSecurityToken(
                issuer: jwtoptions.Issuer,
                audience: jwtoptions.Audience,
                claims:authclaims,
                expires:DateTime.UtcNow.AddDays(jwtoptions.DuringInDay),
                signingCredentials:new SigningCredentials(secretkey,SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
;        }
    }
}
