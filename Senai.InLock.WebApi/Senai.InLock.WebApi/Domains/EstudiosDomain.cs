using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Domains
{
    public class EstudiosDomain
    {
        [Required(ErrorMessage = "O nome do estudio é obrigatório")]
        public int IdEstudio { get; set; }

        public string NomeEstudio { get; set; }
    }
}
