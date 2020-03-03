using Senai.InLock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.InLock.WebApi.Interfaces
{
    /// Interface responsável pelo repositório UsuarioRepository

    interface IUsuariosRepository
    {
        /// Lista todos os usuários

        List<UsuariosDomain> Listar();

        /// Busca um usuários através do ID

        UsuariosDomain BuscarPorId(int id);


        void Cadastrar(UsuariosDomain novoUsuario);

        /// <summary>
        /// Atualiza um usuário existente
        void Atualizar(int id, UsuariosDomain UsuarioAtualizado);

        /// <summary>
        /// Deleta um usuário existente
        void Deletar(int id);

        UsuariosDomain BuscarPorEmailSenha(string email, string senha);
    }
}
