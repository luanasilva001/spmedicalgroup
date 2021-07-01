using senai.sp_medicals.webApi.Domains;
using System.Collections.Generic;

namespace senai.sp_medicals.webApi.Controllers
{
    interface ITipoUsuarioRepository
    {
        List<TipoUsuario> Listar();

        TipoUsuario BuscarPorId(int id);

        void Cadastrar(TipoUsuario novoTipoUsuario);

        void AtualizarPorId(int id, TipoUsuario tipoUsuarioAtualizado);

        void Deletar(int id);
    }
}
