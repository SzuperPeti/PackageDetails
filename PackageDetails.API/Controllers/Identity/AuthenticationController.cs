using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PackageDetails.API.DTO;
using PackageDetails.CORE.Models.Identity;
using PackageDetails.CORE.Services.Identity;

namespace PackageDetails.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;


        public AuthenticationController(IAuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userForRegisterDTO)
        {


            if (await _authService.IsUserExistByEmail(userForRegisterDTO.Email.ToLower().Trim()))
                return BadRequest("User already exist");

            var userToCreate = new User()
            {
                Email = userForRegisterDTO.Email.ToLower().Trim(),
                FirstName = userForRegisterDTO.FirstName.Trim(),
                LastName = userForRegisterDTO.LastName.Trim()
            };

            var createdUser = await _authService.Register(userToCreate, userForRegisterDTO.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserDTO userForLoginDTO)
        {
            var userFromRepo = await _authService.Login(userForLoginDTO.Email.ToLower(), userForLoginDTO.Password);

            if (userFromRepo == null)
                return Unauthorized();


            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Email, userFromRepo.Email),
                new Claim(ClaimTypes.Name, userFromRepo.FirstName + " " + userFromRepo.LastName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }


    }
}
