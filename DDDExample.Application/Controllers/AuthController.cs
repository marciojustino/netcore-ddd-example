using DDDExample.Domain.Dtos;
using DDDExample.Domain.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DDDExample.Application.Controllers
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
            var authorizedUserResult = await _authService.LoginAsync(auth);
            if (authorizedUserResult.IsSuccess)
            {
                return Ok(new LoggedUserDto
                {
                    UserName = authorizedUserResult.Value.Name,
                    Email = authorizedUserResult.Value.Email?.Value,
                    //LastLoggedAt = authorizedUserResult.Value.LastLoggedAt,
                    //Status = authorizedUserResult.Value.Status,
                    UserId = authorizedUserResult.Value.Id,
                });
            }

            return new UnauthorizedObjectResult(new { message = authorizedUserResult.Error });
        }
    }
}
