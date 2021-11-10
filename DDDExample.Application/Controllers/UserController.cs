namespace DDDExample.Application.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Service.Validators;

    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IBaseService<User> _userService;

        public UserController(IBaseService<User> userService) => _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetUsers() => ExecuteAsync(async () => await _userService.GetAsync());

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            return user is null ? BadRequest(nameof(user)) : Execute(() => _userService.Add<UserValidator>(user));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] User user)
        {
            return Execute(() => _userService.Update<UserValidator>(user));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteUser([FromRoute] Guid id) => ExecuteAsync(async () => await _userService.DeleteAsync(id));

        [HttpGet("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id) => ExecuteAsync(async () => await _userService.GetByIdAsync(id));

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private IActionResult ExecuteAsync(Func<Task> func)
        {
            try
            {
                var result = func();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}