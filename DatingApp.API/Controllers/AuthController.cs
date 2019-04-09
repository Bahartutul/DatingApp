using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
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

        // [HttpPost("login")]
        // public async Task<IActionResult> Login(){

        // }
    }
}