using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class EstudiosRepository : IEstudiosRepository
    {
        private string stringConexao = "Data Source = WIN-T3EDO5059Q\\SQLEXPRESS; initial catalog=InLock_Games_Tarde; integrated security=true;";
        //private string stringConexao = "Data Source=DEV7\\SQLEXPRESS; initial catalog=InLock_Games_Tarde ; user Id=sa; pwd=sa@132";

        public List<EstudiosDomain> ListarEstudios()
        {
            List<EstudiosDomain> estudios = new List<EstudiosDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string queryEstudios = "SELECT IdEstudio, NomeEstudio FROM Estudios";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para receber os dados do banco de dados
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryEstudios, con))
                {
                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para serem lidos no rdr, o laço se repete
                    while (rdr.Read())
                    {
                        // Instancia um objeto funcionario 
                        EstudiosDomain estudio = new EstudiosDomain
                        {
                            IdEstudio = Convert.ToInt32(rdr["IdEstudio"])
                            ,
                            NomeEstudio = rdr["NomeEstudio"].ToString()
                        };

                        estudios.Add(estudio);
                    }
                }
            }
            return estudios;
        }


        public void CadastrarEstudios(EstudiosDomain novoEstudio)
        {
            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryInsert = "INSERT INTO Estudios(NomeEstudio) " +
                                     "VALUES (@NomeEstudio)";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@NomeEstudio", novoEstudio.NomeEstudio);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Deleta um estudio existente
        /// </summary>
        /// <param name="id"></param>
        public void DeletarEstudio(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada passando o valor como parâmetro
                string queryDelete = "DELETE FROM Estudios WHERE IdEstudio = @ID";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Busca um funcionário através do ID
        /// </summary>
        /// <param name="id">ID do funcionário que será buscado</param>
        /// <returns>Retorna um funcionário buscado</returns>
        public EstudiosDomain BuscarPorId(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT IdEstudio, NomeEstudio FROM Estudios" +
                                        " WHERE IdEstudio = @ID";

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
                        // Instancia um objeto funcionario 
                        EstudiosDomain estudio = new EstudiosDomain
                        {
                            // Atribui à propriedade IdFuncionario o valor da coluna "IdFuncionario" da tabela do banco
                            IdEstudio = Convert.ToInt32(rdr["IdEstudio"])
                            ,
                            NomeEstudio = rdr["NomeEstudio"].ToString()
                        };

                        // Retorna o funcionário buscado
                        return estudio;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }
    }
}
