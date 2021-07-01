using senai.sp_medicals.webApi.Domains;
using System.Collections.Generic;

namespace senai.sp_medicals.webApi.Controllers
{
    interface IPacienteRepository
    {
        List<Paciente> Listar();

        Paciente BuscarPorId(int id);

        Paciente Cadastrar(Paciente novoPaciente);

        void AtualizarPorId(int id, Paciente pacienteAtualizado);

        List<Paciente> ListarConsultas(int id);

        void Deletar(int id);
    }
}
