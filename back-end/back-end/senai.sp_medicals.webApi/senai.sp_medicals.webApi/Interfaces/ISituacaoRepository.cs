using senai.sp_medicals.webApi.Domains;
using System.Collections.Generic;

namespace senai.sp_medicals.webApi.Controllers
{
    interface ISituacaoRepository
    {
        List<Situacao> Listar();

        Situacao BuscarPorId(int id);

        void Cadastrar(Situacao novaSituacao);

        void AtualizarPorId(int id, Situacao situacaoAtualiza);

        void Deletar(int id);
    }
}
