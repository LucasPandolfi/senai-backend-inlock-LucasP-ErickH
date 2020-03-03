using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class JogosRepository : IJogosRepository
    {

        private string stringConexao = "Data Source=WIN-T3EDO5059Q\\SQLEXPRESS; initial catalog= InLock_Games_Tarde; integrated security=true;";
        //private string stringConexao = "Data Source=DEV7\\SQLEXPRESS; initial catalog=InLock_Games_Tarde ; user Id=sa; pwd=sa@132";

        public List<JogosDomain> ListarJogos()
        {
            List<JogosDomain> jogos = new List<JogosDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string queryJogos = "SELECT IdJogo, NomeJogo, DescricaoJogo, DataLancamento, ValorJogo, Estudios.IdEstudio, Estudios.NomeEstudio FROM Jogos INNER JOIN Estudios ON Estudios.IdEstudio = Jogos.IdEstudio";

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
                        JogosDomain jogo = new JogosDomain
                        {
                            IdJogo = Convert.ToInt32(rdr["IdJogo"])
                            ,
                            NomeJogo = rdr["NomeJogo"].ToString()
                            ,
                            DescricaoJogo = rdr["DescricaoJogo"].ToString()
                            ,
                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"])
                            ,
                            ValorJogo = Convert.ToInt32(rdr["ValorJogo"])
                            ,
                            IdEstudio = Convert.ToInt32(rdr["IdEstudio"])
                            ,
                            Estudios = new EstudiosDomain()
                            {
                             IdEstudio = Convert.ToInt32(rdr["IdEstudio"])
                             ,
                             NomeEstudio = rdr["NomeEstudio"].ToString()
                            }
                        };

                        jogos.Add(jogo);
                    }
                }
            }
            return jogos;
        }


        public void CadastrarJogos(JogosDomain novoJogo)
        {
            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryInsert = "INSERT INTO Jogos(NomeJogo, DescricaoJogo, DataLancamento, ValorJogo, IdEstudio) " +
                                     "VALUES (@NomeJogo, @DescricaoJogo, @DataLancamento, @ValorJogo, @IdEstudio)";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@NomeJogo", novoJogo.NomeJogo);
                    cmd.Parameters.AddWithValue("@DescricaoJogo", novoJogo.DescricaoJogo);
                    cmd.Parameters.AddWithValue("@DataLancamento", novoJogo.DataLancamento);
                    cmd.Parameters.AddWithValue("@ValorJogo", novoJogo.ValorJogo);
                    cmd.Parameters.AddWithValue("@IdEstudio", novoJogo.IdEstudio);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Deleta um jogo existente
        /// </summary>
        /// <param name="id"></param>
        public void DeletarJogo(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada passando o valor como parâmetro
                string queryDelete = "DELETE FROM Jogos WHERE IdJogo = @ID";

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
        /// Busca um jogo através do ID
        /// </summary>
        /// <param name="id">ID do jogo que será buscado</param>
        /// <returns>Retorna um jogo buscado</returns>
        public JogosDomain BuscarJogoPorId(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT IdJogo, NomeJogo FROM Jogos" +
                                        " WHERE IdJogo = @ID";

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
                        JogosDomain jogo = new JogosDomain
                        {
                            // Atribui à propriedade IdFuncionario o valor da coluna "IdJogo" da tabela do banco
                            IdJogo = Convert.ToInt32(rdr["IdJogo"])
                            ,
                            NomeJogo = rdr["NomeJogo"].ToString()
                        };

                        // Retorna o jogo buscado
                        return jogo;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }
    }
}
