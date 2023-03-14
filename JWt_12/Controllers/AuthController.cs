using JWt_12.Constant;
using JWt_12.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWt_12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            User user = Datas.users.FirstOrDefault(m => m.Name == loginDto.Name && m.Password == loginDto.Password);
            if (user == null)
            {
                return (BadRequest("invalid user credinalts"));
            }
            else
            {
                LoginResponseDTO loginResponseDTO=new LoginResponseDTO()
                { user = user };
                DateTime expirationDate = DateTime.Now.AddHours(3);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.Name.ToString()));
                claims.Add(new Claim(ClaimTypes.Expiration, expirationDate.ToString()));
                claims.Add(new Claim(ClaimTypes.Role,user.Role));

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("TokenSetting:Secret").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expirationDate,
                    SigningCredentials = creds
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenValue = tokenHandler.WriteToken(token);
                loginResponseDTO.Token = tokenValue;
                return Ok(loginResponseDTO);
            };
            
        
           
        }

    }
}
