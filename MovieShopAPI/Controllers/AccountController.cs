using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            var createdUser = await _userService.RegisterUserAsync(model);

            // 200 (Not bad practice)
            //return Ok(createdUser);

            // 201 and send the URL for newly created user also (api/account/user/{id})  (Better practice)
            return CreatedAtRoute(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult>  LoginUserAsync([FromBody] UserLoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            // Cookie-based Authenticaiton
            var loggedInUser = await _userService.LoginAsync(model.Email, model.Password);
            if (loggedInUser == null) return Unauthorized();

            // Token-based Authentication
            // Create JWT Token
            // Download library System.IdentityModel.Tokens.Jwt & Microsoft.IdentityModel.Tokens

            var jwtToken = CreateJWT(loggedInUser);
            return Ok(new { token = jwtToken });

        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userService.GetAllUsersAsync();
            if (user == null) return NotFound($"No user is found.");
            return Ok(user);
        }


        [HttpGet]
        [Route("{id:int}", Name = nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound($"User not found with id = {id}");
            return Ok(user);
        }
        
        private string CreateJWT(UserLoginResponseModel model)
        {
            // We will use the token libraries to create token.
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.GivenName, model.FirstName),
                new Claim(ClaimTypes.Surname, model.LastName)
            };
            var identityClaims = new ClaimsIdentity(claims);

            // Read the secret key from appsettings, make sure the secret key is unique and not guessable
            // In real world, we use something like Azure KeyVault to store any secrets of the application.
            var secretKey = _configuration["JwtSettings:SecretKey"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var expires = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("JwtSettings:Expiration"));

            // Pick an hashing algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Create the token object that you will use to create the token that will include all the information such as credentials,
            // claims, expiration time...
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identityClaims,
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var encodedJwt = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(encodedJwt);            
        }
    }
}
