using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo,IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userDto)
        {
            userDto.username=userDto.username.ToLower();
            if(await _repo.UserExists(userDto.username)){
                return BadRequest("User Exists");
            }
          var  userobj=new User{
              username=userDto.username
          };
          var createdUser=await _repo.Register(userobj,userDto.password);
          return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLogin user){
           var userExists=await _repo.Login(user.username.ToLower(),user.password);
           if(userExists==null){
               return Unauthorized();
           }

           var claims=new[]
           {
              new Claim(ClaimTypes.NameIdentifier,userExists.id.ToString()),
              new Claim(ClaimTypes.Name,userExists.username)
           };

           var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
           var keyCredential=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

           var TokenDescriptor=new SecurityTokenDescriptor{
               Subject=new ClaimsIdentity(claims),
               Expires=DateTime.Now.AddDays(1),
               SigningCredentials=keyCredential
           };
           var tokenHandler=new JwtSecurityTokenHandler();
          var token=tokenHandler.CreateToken(TokenDescriptor);
          return Ok(new{
              token=tokenHandler.WriteToken(token)
          });
        }
    }
}