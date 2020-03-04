using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using Senai.InLock.WebApi.Repositories;

namespace Senai.InLock.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints referentes aos funcionarios
    /// </summary>

    // Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    // Define que a rota de uma requisição será no formato domínio/api/NomeController
    [Route("api/[controller]")]

    [ApiController]
    public class TiposUsuariosController : ControllerBase
    {
        /// <summary>
        /// Cria um objeto _usuarioRepository que irá receber todos os métodos definidos na interface
        /// </summary>
        private ITiposUsuariosRepository _tiposUsuariosRepository { get; set; }

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public TiposUsuariosController()
        {
            _tiposUsuariosRepository = new TiposUsuariosRepository();
        }


        /// <summary>
        /// Lista todos os TiposUsuarios
        /// </summary>
        /// <returns>Retorna uma lista de tiposUsuarios e um status code 200 - Ok</returns>
        /// dominio/api/Jogos
        [Authorize(Roles = "1")]    // Somente o tipo de usuário 1 (administrador) pode acessar o endpoint
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(_tiposUsuariosRepository.ListarTipoUsuario());
        }


        /// <summary>
        /// Busca um tipoUsuario através do seu ID
        /// </summary>
        /// <param name="id">ID do jogo que será buscado</param>
        /// <returns>Um tipoUsuario buscado ou NotFound caso nenhum seja encontrado</returns>
        /// dominio/api/Jogos/1
        [Authorize(Roles = "1, 2")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Cria um objeto jogoBuscado que irá receber o jogo buscado no banco de dados
            TiposUsuariosDomain tipoUsuarioBuscado = _tiposUsuariosRepository.BuscarPorId(id);

            // Verifica se algum jogo foi encontrado
            if (tipoUsuarioBuscado != null)
            {
                // Caso seja, retorna os dados buscados e um status code 200 - Ok
                return Ok(tipoUsuarioBuscado);
            }

            // Caso não seja, retorna um status code 404 - NotFound com a mensagem
            return NotFound("Nenhum tipoUsuario encontrado para o identificador informado");
        }

    }
}