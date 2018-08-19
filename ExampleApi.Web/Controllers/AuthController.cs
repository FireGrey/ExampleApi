using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExampleApi.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExampleApi.Web.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        public AuthController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Generate a token for a given user with valid credentials
        /// </summary>
        /// <param name="credentials">Username and Password to authenticate with</param>
        /// <returns>Returns the generated token</returns>
        [HttpPost("token")]
        public ActionResult Token(Credentials credentials)
        {
            // Hardcoded until there's a user store
            if (!(credentials.UserName == "admin" && credentials.Password == "admin"))
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, credentials.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey)), _jwtSettings.SigningAlgorithm)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new { token = encodedJwt });
        }
    }
}