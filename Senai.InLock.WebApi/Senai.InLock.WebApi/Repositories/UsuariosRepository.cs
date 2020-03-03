using senai.InLock.WebApi.Interfaces;
using Senai.InLock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Peoples.WebApi.Repositories
{
    /// <summary>
    /// Repositório dos Usuários
    /// </summary>
    public class UsuariosRepository : IUsuariosRepository
    {

        private string stringConexao = "Data Source=DEV7\\SQLEXPRESS; initial catalog=InLock_Games_Tarde ; user Id=sa; pwd=sa@132";


        public void Atualizar(int id, UsuariosDomain UsuarioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryUpdate = "UPDATE Usuarios " +
                                     "SET Email = @Email, Senha = @Senha, IdTipoUsuario = @IdTipoUsuario " +
                                     "WHERE IdTipoUsuario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Email", UsuarioAtualizado.Email);
                    cmd.Parameters.AddWithValue("@Senha", UsuarioAtualizado.Senha);
                    cmd.Parameters.AddWithValue("@IdTipoUsuario", UsuarioAtualizado.IdTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// Cadastra um novo usuário
        public void Cadastrar(UsuariosDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Usuarios(Email, Senha, IdTipoUsuario) " +
                                     "VALUES (@Email, @Senha, @IdTipoUsuario)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Email", novoUsuario.Email);
                    cmd.Parameters.AddWithValue("@Senha", novoUsuario.Senha);
                    cmd.Parameters.AddWithValue("@IdTipoUsuario", novoUsuario.IdTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Usuarios WHERE IdUsuario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// Lista todos os usuários

        public List<UsuariosDomain> Listar()
        {
            // Cria uma lista usuários onde serão armazenados os dados
            List<UsuariosDomain> usuarios = new List<UsuariosDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT U.IdUsuario, U.Email, U.IdTipoUsuario, TU.Titulo FROM Usuarios U INNER JOIN TiposUsuario TU ON U.IdTipoUsuario = TU.IdTipoUsuario";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UsuariosDomain usuario = new UsuariosDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"])
                            ,
                            Email = rdr["Email"].ToString()
                            ,
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            TipoUsuario = new TiposUsuariosDomain
                            {
                                IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                                ,
                                NomeTipoUsuario = rdr["NomeTipoUsuario"].ToString()
                            }
                        };

                        usuarios.Add(usuario);
                    }
                }
            }

            return usuarios;
        }


        /// <summary>
        /// Busca um jogo através do ID
        /// </summary>
        /// <param name="id">ID do jogo que será buscado</param>
        /// <returns>Retorna um jogo buscado</returns>
        public UsuariosDomain BuscarPorId(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT IdUsuario, Email, IdTipoUsuario FROM Jogos" +
                                        " WHERE IdUsuario = @ID";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para receber os dados do banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Caso o resultado da query possua registro
                    if (rdr.Read())
                    {
                        // Instancia um objeto jogo 
                        UsuariosDomain usuario = new UsuariosDomain
                        {
                            // Atribui à propriedade IdFuncionario o valor da coluna "IdJogo" da tabela do banco
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"])
                            ,
                            Email = rdr["Email"].ToString()
                            ,
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                        };

                        // Retorna o jogo buscado
                        return usuario;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }


        /// <summary>
        /// Valida o usuário
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>Retorna um usuário validado</returns>
        public UsuariosDomain BuscarPorEmailSenha(string email, string senha)
        {
            // Define a conexão passando a string
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Define a query a ser executada no banco
                string querySelect = "SELECT U.IdUsuario, U.Email, U.IdTipoUsuario, TU.Titulo FROM Usuarios U INNER JOIN TiposUsuario TU ON U.IdTipoUsuario = TU.IdTipoUsuario WHERE Email = @Email AND Senha = @Senha";

                // Define o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    // Define o valor dos parâmetros
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    // Abre a conexão com o banco
                    con.Open();

                    // Executa o comando e armazena os dados no objeto rdr
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Caso o resultado da query possua registro
                    if (rdr.Read())
                    {
                        // Instancia um objeto usuario 
                        UsuariosDomain usuario = new UsuariosDomain
                        {
                            // Atribui às propriedades os valores das colunas da tabela do banco
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"])
                            ,
                            Email = rdr["Email"].ToString()
                            ,
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                            ,
                            TipoUsuario = new TiposUsuariosDomain
                            {
                                IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                                ,
                                NomeTipoUsuario = rdr["Titulo"].ToString()
                            }
                        };

                        // Retorna o usuario buscado
                        return usuario;
                    }
                }

                // Caso não encontre um email e senha correspondente, retorna null
                return null;
            }
        }
    }
}
