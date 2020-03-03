using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.InLock.WebApi.Interfaces;
using senai.Peoples.WebApi.Repositories;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.ViewModels;

namespace senai.InLock.WebApi.Controllers
{

    [Produces("application/json")]

    [Route("api/[controller]")]

    // Define que é um controlador de API
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Cria um objeto _usuarioRepository que irá receber todos os métodos definidos na interface
        /// </summary>
        private IUsuariosRepository _usuarioRepository { get; set; }

        public LoginController()
        {
            _usuarioRepository = new UsuariosRepository();
        }
        
        [HttpPost]
        public IActionResult Post(LoginViewModel login)
        {
            // Busca o usuário pelo e-mail e senha
            UsuariosDomain usuarioBuscado = _usuarioRepository.BuscarPorEmailSenha(login.Email, login.Senha);


            if (usuarioBuscado == null)
            {

                return NotFound("E-mail ou senha inválidos");
            }

            // Caso o usuário seja encontrado, prossegue para a criação do token Instalar as dependências: Criar e validar o jwt
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("InLock-chave-autenticacao"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Gera o token
            var token = new JwtSecurityToken(
                issuer: "InLock.WebApi",                // emissor do token
                audience: "InLock.WebApi",              // destinatário do token
                claims: claims,                          // dados definidos acima
                expires: DateTime.Now.AddMinutes(30),    // tempo de expiração
                signingCredentials: creds                // credenciais do token
            );

            // Retorna Ok 
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
