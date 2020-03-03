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

    // Define que é um controlador de API
    [ApiController]

    public class JogosController : ControllerBase
    {
        private IJogosRepository _jogoRepository { get; set; }

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public JogosController()
        {
            _jogoRepository = new JogosRepository();
        }

        /// <summary>
        /// Lista todos os jogos
        /// </summary>
        /// <returns>Retorna uma lista de jogos e um status code 200 - Ok</returns>
        /// dominio/api/Jogos
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            return Ok(_jogoRepository.ListarJogos());
        }

        /// <summary>
        /// Cadastra um novo estudio
        /// </summary>
        /// <param name="novoJogo">Objeto novoJogo que será cadastrado</param>
        /// <returns>Retorna os dados que foram enviados para cadastro e um status code 201 - Created</returns>
        /// dominio/api/Jogos
        [Authorize(Roles = "1")]    // Somente o tipo de usuário 1 (administrador) pode acessar o endpoint
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(JogosDomain novoJogo)
        {
            if (novoJogo.NomeJogo == null)
            {
                return BadRequest("O nome do jogo é obrigatório!");
            }
            // Faz a chamada para o método .Cadastrar();
            _jogoRepository.CadastrarJogos(novoJogo);

            // Retorna o status code 201 - Created com a URI e o objeto cadastrado
            return Created("http://localhost:5000/api/Funcionarios", novoJogo);
        }


        /// <summary>
        /// Deleta um jogo
        /// </summary>
        /// <param name="id">ID do jogo que será deletado</param>
        /// <returns>Retorna um status code com uma mensagem de sucesso ou erro</returns>
        /// dominio/api/Jogos/1
        [Authorize(Roles = "1")]    // Somente o tipo de usuário 1 (administrador) pode acessar o endpoint
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            // Cria um objeto funcionarioBuscado que irá receber o funcionário buscado no banco de dados
            JogosDomain jogoBuscado = _jogoRepository.BuscarJogoPorId(id);

            // Verifica se o funcionário foi encontrado
            if (jogoBuscado != null)
            {
                // Caso seja, faz a chamada para o método .Deletar()
                _jogoRepository.DeletarJogo(id);

                // e retorna um status code 200 - Ok com uma mensagem de sucesso
                return Ok($"O jogo {id} foi deletado com sucesso!");
            }

            // Caso não seja, retorna um status code 404 - NotFound com a mensagem
            return NotFound("Nenhum jogo encontrado para o identificador informado");
        }
    }
}