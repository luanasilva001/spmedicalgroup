using senai.sp_medicals.webApi.Domains;
using System.Collections.Generic;

namespace senai.sp_medicals.webApi.Controllers
{
    interface IMedicoRepository
    {
        List<Medico> Listar();

        Medico BuscarPorId(int id);

        void Cadastrar(Medico novaMedico);

        void AtualizarPorId(int id, Medico medicoAtualizado);

        void Deletar(int id);

        List<Medico> ListarConsultas(int id);
    }
}
