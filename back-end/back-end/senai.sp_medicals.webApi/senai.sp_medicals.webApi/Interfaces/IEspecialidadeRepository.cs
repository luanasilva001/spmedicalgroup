using senai.sp_medicals.webApi.Domains;
using System.Collections.Generic;

namespace senai.sp_medicals.webApi.Controllers
{
    interface IEspecialidadeRepository
    {
        List<Especialidade> Listar();

        List <Especialidade> ListarCadastro(int id);

        Especialidade BuscarPorId(int id);

        Especialidade Cadastrar(Especialidade novaEspecialidade);

        void AtualizarPorId(int id, Especialidade especialidadeAtualizada);

        void Deletar(int id);
    }
}
