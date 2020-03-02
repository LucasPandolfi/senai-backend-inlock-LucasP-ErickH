using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Domains
{
    public class Jogos
    {
        public int IdJogo { get; set; }

        public string NomeJogo { get; set; }

        public string DescricaoJogo { get; set; }

        public DateTime DataLancamento { get; set; }

        public double ValorJogo { get; set; }

        public int IdEstudio { get; set; }
    }
}
