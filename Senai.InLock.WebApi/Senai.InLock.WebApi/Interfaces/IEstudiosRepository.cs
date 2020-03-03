using Senai.InLock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Interfaces
{
    interface IEstudiosRepository
    {
        List<EstudiosDomain> ListarEstudios();

        void CadastrarEstudios(EstudiosDomain novoEstudio);

        void DeletarEstudio(int id);

        EstudiosDomain BuscarPorId(int id);
    }
}
