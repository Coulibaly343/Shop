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

        [HttpPost ("user")]
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

        // [HttpPost ("login")]
        // public async Task<IActionResult> Login ([FromBody] SignInUser command) {
        //     if (!ModelState.IsValid)
        //         return BadRequest (ModelState);
        //     var user = await _authService.LoginAsync (command.Email, command.Password);
        //     if (user == null)
        //         return Unauthorized ();
        //     var token = new TokenDTO {
        //         Token = await GenerateToken (user, _jwtSettings)
        //     };
        //     return Ok (token);
        // }
        // private async Task<string> GenerateToken (User user, IJWTSettings jwtSettings) {
        //     var tokenHandler = new JwtSecurityTokenHandler ();
        //     var key = Encoding.ASCII.GetBytes (jwtSettings.Key);
        //     var tokenDescriptor = new SecurityTokenDescriptor {
        //         Subject = new ClaimsIdentity (new Claim[] {
        //         new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
        //         new Claim (ClaimTypes.Name, user.Email),
        //         new Claim (ClaimTypes.Role, user.Role)
        //         }),
        //         Issuer = "",
        //         Expires = DateTime.Now.AddDays (jwtSettings.ExpiryDays),
        //         SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key),
        //         SecurityAlgorithms.HmacSha512Signature)
        //     };
        //     var token = tokenHandler.CreateToken (tokenDescriptor);
        //     return await Task.FromResult (tokenHandler.WriteToken (token));
        // }
    }
}