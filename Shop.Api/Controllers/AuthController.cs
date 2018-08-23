using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure.Command.Admin;
using Shop.Infrastructure.Command.User;
using Shop.Infrastructure.Services.Interfaces;

namespace Shop.Api.Controllers {
    public class AuthController : ApiUserController {
        private readonly IAuthService _authService;

        public AuthController (IAuthService authService) {
            _authService = authService;
        }

        [HttpPost ("admin")]
        public async Task<IActionResult> RegisterAdmin ([FromBody] RegisterAdmin command) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);
            try {
                await _authService.RegisterAdminAsync (command.Name, command.Surname, command.Email, command.Password, command.FlatNumber,
                    command.StreetNumber, command.Street, command.City, command.ZipCode);
                return StatusCode (201);
            } catch (Exception e) {
                return BadRequest (e.Message);
            }
        }

        [HttpPost ("admin")]
        public async Task<IActionResult> RegisterUser ([FromBody] RegisterUser command) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);
            try {
                await _authService.RegisterUserAsync (command.Name, command.Surname, command.Email, command.Password, command.FlatNumber,
                    command.StreetNumber, command.Street, command.City, command.ZipCode);
                return StatusCode (201);
            } catch (Exception e) {
                return BadRequest (e.Message);
            }
        }
    }
}