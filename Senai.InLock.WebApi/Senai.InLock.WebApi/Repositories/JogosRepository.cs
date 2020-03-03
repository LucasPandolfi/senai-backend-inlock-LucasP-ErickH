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
        private string StringConexao = "Data Source=DESKTOP-NJ6LHN1\\SQLDEVELOPER; initial catalog=Peoples; integrated security=true";

        public List<JogosDomain> ListarJogos()
        {
            public List<JogosDomain> jogos = new List<JogosDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a intrução a ser executada
                string queryJogos = "SELECT IdJogo, NomeJogo, DescricaoJogo, DataLancamento, IdEstudio FROM Jogos";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para receber os dados do banco de dados
                SqlDataReader rdr;
}
                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(queryJogos, con))
                {
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para serem lidos no rdr, o laço se repete
                    while (rdr.Read())
                    {
                        // Instancia um objeto funcionario 
                        JogosDomain jogo = new JogosDomain
                        {
                            // Atribui à propriedade IdJogo o valor da coluna "IdJogo" da tabela do banco
                            IdJogo = Convert.ToInt32(rdr["IdJogo"])

                            // Atribui à propriedade NomeJogo o valor da coluna "NomeJogo" da tabela do banco
                            ,
                            NomeJogo = rdr["NomeJogo"].ToString()

                            // Atribui à propriedade DescricaoJogo o valor da coluna "DescricaoJogo" da tabela do banco
                            ,
                            DescricaoJogo = rdr["DescricaoJogo"].ToString()

                            // Atribui à propriedade DataLancamento o valor da coluna "DataLancamento" da tabela do banco
                            ,
                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"])

                            //Atribui à propriedade IdEstudio o valor da coluna "IdEstudio" da tabela do banco
                            ,
                            IdEstudio = Convert.ToInt32(rdr["IdEstudio"])
                        };

                            // Adiciona o funcionario criado à lista funcionarios
                            jogos.Add(jogo);
                    }
                }
            }

            // Retorna a lista de funcionarios
            return jogos;
            }
        }                
    }
}
