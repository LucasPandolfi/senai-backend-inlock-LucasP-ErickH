using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Domains
{
    public class JogosDomain
    {
        public int IdJogo { get; set; }

        [Required(ErrorMessage = "O nome do jogo é obrigatório")]
        public string NomeJogo { get; set; }

        public string DescricaoJogo { get; set; }

        [Required(ErrorMessage = "A data de lançamento do jogo é obrigatório")]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "O valor do jogo é obrigatório")]
        public double ValorJogo { get; set; }

        [Required(ErrorMessage = "O id do estudio é obrigatório")]
        public int IdEstudio { get; set; }

        public EstudiosDomain Estudios { get; set;}
    }
}
