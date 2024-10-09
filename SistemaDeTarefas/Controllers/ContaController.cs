using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ContaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if(login.Login == "admin" &&  login.Password == "admin")
            {
                var token = GerarTokenJWT();
                return Ok(new { token });
            }
            return BadRequest(new { message = "Credenciais inválidas. Por favor, verifique seu nome de usuário e senha." });
        }

        private string GerarTokenJWT()
        {
            var secretKey = _configuration["Jwt:SecretKey"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", "admin"),
                new Claim("nome", "Administrador do Sistema")
            };

            var token = new JwtSecurityToken(
                issuer: "sua_empresa",
                audience: "sua_aplicacao",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credencial
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
