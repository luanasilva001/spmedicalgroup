using senai.sp_medicals.webApi.Domains;
using System.Collections.Generic;

namespace senai.sp_medicals.webApi.Controllers
{
    interface IClinicaRepository
    {
        List<Clinica> Listar();

        Clinica BuscarPorId(int id);

        void Cadastrar(Clinica novaClinica);

        void AtualizarPorId(int id, Clinica clinicaAtualizada);

        void Deletar(int id);
    }
}
