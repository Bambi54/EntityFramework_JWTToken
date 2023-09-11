using CW8.Models;
using CW9.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CW9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly Cw8DBContext _context;
        public AccountsController(IConfiguration configuration, Cw8DBContext cw8DBContext)
        {
            _config = configuration;
            _context = cw8DBContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequestDTO dto)
        {

            if (await _context.Accounts.AnyAsync(e => e.Username == dto.Login))
            {
                return Conflict();
            }

            await _context.Accounts.AddAsync(new Account
            {
                Username = dto.Login,
                Password = HashPass(dto.Password),
            });

            await _context.SaveChangesAsync();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!)),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            var stringifiedtoken = tokenHandler.WriteToken(token);

            var refTokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _config["JWT:RefIssuer"],
                Audience = _config["JWT:RefAudience"],
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:RefKey"]!)),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var refToken = tokenHandler.CreateToken(refTokenDescription);
            var stringifiedRefToken = tokenHandler.WriteToken(refToken);

            return Ok(new
            {
                Token = stringifiedtoken,
                RefreshToken = stringifiedRefToken
            });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO dto)
        {
            var acc = await _context.Accounts.FirstAsync(e => e.Username == dto.Login);

            if(!await _context.Accounts.AnyAsync(e => e.Username == dto.Login) || !VerifyPass(dto.Password, HashPass(acc.Password)))
            {
                return NotFound();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!)),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            var stringifiedtoken = tokenHandler.WriteToken(token);

            var refTokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _config["JWT:RefIssuer"],
                Audience = _config["JWT:RefAudience"],
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:RefKey"]!)),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var refToken = tokenHandler.CreateToken(refTokenDescription);
            var stringifiedRefToken = tokenHandler.WriteToken(refToken);

            return Ok(new
            {
                Token = stringifiedtoken,
                RefreshToken = stringifiedRefToken
            });
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken(RefreshTokenDTO refreshtoken)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(refreshtoken.RefToken, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["JWT:RefIssuer"],
                    ValidAudience = _config["JWT:RefAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:RefKey"]!))
                }, out SecurityToken validatedToken);
                return Ok(true + " " + validatedToken);
            }
            catch
            {
                return Unauthorized();
            }

        }

        private string HashPass(string password)
        {
            var passwordHasher = new PasswordHasher<Account>();
            return passwordHasher.HashPassword(new Account(), password);
        }

        private bool VerifyPass(string password, string hash)
        {
            var passwordHasher = new PasswordHasher<Account>();
            return passwordHasher.VerifyHashedPassword(new Account(), hash, password) == PasswordVerificationResult.Success;
        }


    }
}
