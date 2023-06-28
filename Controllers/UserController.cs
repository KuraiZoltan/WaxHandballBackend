using Angular_Test_App.Model;
using Angular_Test_App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Angular_Test_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public UserController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("addUser")]
        public async Task AddUser([FromBody] UserRegistrationTemp user)
        {
            await _userService.AddUser(user);
        }

        [HttpPost]
        [Route("loginUser")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials loginData)
        {
            if (await _userService.LoginIsValid(loginData))
            {
                var claims = await _userService.CreateClaims(loginData);

                var expireTime = DateTime.UtcNow.AddHours(1);

                return Ok(new
                {
                    access_token = CreateToken(claims, expireTime),
                    expiresAt = expireTime,
                });
            }

            ModelState.AddModelError("Unauthorized", "Something went wrong.");
            return Unauthorized(ModelState);

        }

        private string CreateToken(IEnumerable<Claim> claims, DateTime expiresAt)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                    issuer: "https://localhost:7043",
                    audience: "https://localhost:7043",
                    claims: claims,
                    expires: expiresAt,
                    signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
