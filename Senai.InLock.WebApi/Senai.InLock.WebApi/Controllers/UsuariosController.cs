using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.InLock.WebApi.Interfaces;
using senai.Peoples.WebApi.Repositories;
using Senai.InLock.WebApi.Domains;

namespace senai.InLock.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints referentes aos usuários
    /// </summary>

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class UsuariosController : ControllerBase
    {
        /// <summary>
        /// Cria um objeto _usuarioRepository que irá receber todos os métodos definidos na interface
        /// </summary>
        private IUsuariosRepository _usuarioRepository { get; set; }

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public UsuariosController()
        {
            _usuarioRepository = new UsuariosRepository();
        }

        /// Lista todos os usuários

        [Authorize(Roles = "1")]    // Somente o tipo de usuário 1 (administrador) pode acessar o endpoint
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(_usuarioRepository.Listar());
        }

        /// <summary>
        /// Cadastra um novo usuário

        [HttpPost]
        public IActionResult Post(UsuariosDomain novoUsuario)
        {
            _usuarioRepository.Cadastrar(novoUsuario);

            return Created("http://localhost:5000/api/", novoUsuario);
        }



        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        /// <param name="id">ID do usuário que será atualizado</param>
        /// <param name="usuarioAtualizado">Objeto usuarioAtualizado que será atualizado</param>
        /// <returns>Retorna um status code</returns>
        /// dominio/api/Usuarios/1
        [Authorize(Roles = "1")]    // Somente o tipo de usuário 1 (administrador) pode acessar o endpoint
        [HttpPut("{id}")]
        public IActionResult Put(int id, UsuariosDomain usuarioAtualizado)
        {
            // Cria um objeto usuarioBuscado que irá receber o usuário buscado no banco de dados
            UsuariosDomain usuarioBuscado = _usuarioRepository.BuscarPorId(id); ///////////////////////MUDAR

            if (usuarioBuscado != null)
            {
                try
                {
                    _usuarioRepository.Atualizar(id, usuarioAtualizado);

                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }

            }


            return NotFound
                (
                    new
                    {
                        mensagem = "Usuário não encontrado",
                        erro = true
                    }
                );
        }
    }
}
