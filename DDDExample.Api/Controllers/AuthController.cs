using DDDExample.Domain.Dtos;
using DDDExample.Domain.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DDDExample.Api.Controllers
{
    [Route("v1/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] AuthDto auth)
        {
            var loginResult = await _authService.LoginAsync(auth);
            if (loginResult.IsSuccess)
            {
                return Ok(new LoggedUserDto
                {
                    UserName = loginResult.Value.Name,
                    Email = loginResult.Value.Email?.Value,
                    LastLoggedAt = loginResult.Value.LastLoggedAt,
                    Status = loginResult.Value.Status,
                    UserId = loginResult.Value.Id,
                });
            }

            return new UnauthorizedObjectResult(new { message = loginResult.Error });
        }
    }
}
