namespace DDDExample.Api.Controllers
{
    using DDDExample.Domain.Dtos;
    using DDDExample.Domain.ValueObjects;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Service.Validators;
    using System;
    using System.Collections.Generic;

    [Route("v1/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IBaseService<User> _userService;

        public UserController(IBaseService<User> userService) => _userService = userService;

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.Get();
            var dtoUsers = new List<UserResponseDto>();
            users.ForEach(u => dtoUsers.Add(new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email.Value,
                Status = u.Status,
                LastLoggedAt = u.LastLoggedAt.GetValueOrDefault(),
            }));
            return Ok(dtoUsers);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if (userDto is null)
                return BadRequest(nameof(userDto));

            var user = new User(userDto.Name, userDto.Email, userDto.Password, userDto.Salt);
            return Execute(() => _userService.Add<UserValidator>(user));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto userDto)
        {
            var user = _userService.GetById(id);
            if (user is null)
                return NotFound();

            user.Name = userDto.Name;
            user.Email = new Email(userDto.Email);
            return Execute(() => _userService.Update<UserValidator>(user));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            try
            {
                _userService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                return Execute(() => _userService.GetById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private IActionResult Execute(Func<object> func)
        {
            var result = func();
            return Ok(result);
        }
    }
}