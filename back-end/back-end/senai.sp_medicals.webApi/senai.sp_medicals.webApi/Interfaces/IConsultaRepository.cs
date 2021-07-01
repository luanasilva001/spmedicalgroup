using senai.sp_medicals.webApi.Domains;
using System.Collections.Generic;

namespace senai.sp_medicals.webApi.Controllers
{
    interface IConsultaRepository
    {

        int BuscarIdPaciente(int id);

        int BuscarIdMedico(int id);

        Consultum BuscarPorId(int id);

        void MudarDescricao(int id, Consultum status);

        void AprovarRecusar(int id, string status);

        void Cadastrar(Consultum novaConsulta);

        void AtualizarPorId(int id, Consultum consultaAtualiza);
        
        void Deletar(int id);

        List<Consultum> ListarTudo();

        List<Consultum> ListarConsultasPaciente(int id);

        List<Consultum> ListarConsultasMedico(int id);
    }
}
