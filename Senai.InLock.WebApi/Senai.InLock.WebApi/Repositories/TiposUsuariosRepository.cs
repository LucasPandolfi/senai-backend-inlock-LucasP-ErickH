using Senai.InLock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class TiposUsuariosRepository
    {
        //private string stringConexao = "Data Source=DEV7\\SQLEXPRESS; initial catalog=InLock_Games_Tarde ; user Id=sa; pwd=sa@132";
        private string stringConexao = "Data Source=WIN-T3EDO5059Q\\SQLEXPRESS; initial catalog= InLock_Games_Tarde; integrated security=true;";


        public List<TiposUsuariosDomain> ListarTipoUsuario()
        {
            List<TiposUsuariosDomain> tiposUsuarios = new List<TiposUsuariosDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string queryJogos = "SELECT IdTipoUsuario, NomeTipoUsuario FROM TiposUsuarios";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para receber os dados do banco de dados
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryJogos, con))
                {
                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para serem lidos no rdr, o laço se repete
                    while (rdr.Read())
                    {
                        // Instancia um objeto funcionario 
                        TiposUsuariosDomain tipoUsuario = new TiposUsuariosDomain
                        {
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                            ,
                            NomeTipoUsuario = rdr["NomeTipoUsuario"].ToString()
                        };

                        tiposUsuarios.Add(tipoUsuario);
                    }
                }
            }
            return tiposUsuarios;
        }



        /// <summary>
        /// Busca um tipo de usuário através do ID
        /// </summary>
        /// <param name="id">ID do tipo de usuário que será buscado</param>
        /// <returns>Retorna um tipo de usuário buscado</returns>
        public TiposUsuariosDomain BuscarPorId(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT IdTipoUsuario, NomeTipoUsuario FROM TiposUsuarios WHERE IdTipoUsuario = @ID";

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
                        // Instancia um objeto tipoUsuario 
                        TiposUsuariosDomain tipoUsuario = new TiposUsuariosDomain
                        {
                            // Atribui às propriedades os valores das colunas da tabela do banco
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                            ,
                            NomeTipoUsuario = rdr["NomeTipoUsuario"].ToString()
                        };

                        // Retorna o tipoUsuario buscado
                        return tipoUsuario;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }
    }
}
