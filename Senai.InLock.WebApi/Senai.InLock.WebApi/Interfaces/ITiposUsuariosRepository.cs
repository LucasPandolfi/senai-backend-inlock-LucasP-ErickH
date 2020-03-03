using Senai.InLock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Interfaces
{
    interface ITiposUsuariosRepository
    {
        List<TiposUsuariosDomain> ListarTipoUsuario();

        TiposUsuariosDomain BuscarPorId(int id);
    }
}
