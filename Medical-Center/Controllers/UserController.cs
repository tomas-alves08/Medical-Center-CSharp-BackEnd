using Medical_Center.Data.Repository.IRepository;
using Medical_Center_Common.Models.DTO.UserData.LocalUserData;
using Medical_Center_Common.Models.DTO.UserData.LoginData;
using Medical_Center_Common.Models.DTO.UserData.RegistrationData;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Center.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepository.LoginAsync(model);

            if(loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token)) 
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }

            return Ok(loginResponse);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            bool ifUserNameUnique = _userRepository.IsUniqueUser(model.UserName);
            
            if (!ifUserNameUnique)
            {
                return BadRequest("Username already exists.");
            }

            var user = await _userRepository.RegisterAsync(model);

            if(user == null) 
            {
                return BadRequest("Error while registering.");
            }

            return Ok();
        }
    }
}
