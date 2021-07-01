using Microsoft.EntityFrameworkCore;
using senai.sp_medicals.webApi.Contexts;
using senai.sp_medicals.webApi.Controllers;
using senai.sp_medicals.webApi.Domains;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Classe responsável pelo repositório das especialidades
/// </summary>
namespace senai.sp_medicals.webApi.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        /// <summary>
        /// Objeto contexto por onde serão chamados os metódos do EF Core
        /// </summary>
        MedicalsContext ctx = new MedicalsContext();

        public void AtualizarPorId(int id, Medico medicoAtualizado)
        {
            Medico medicoBuscado = ctx.Medicos.Find(id);

            if (medicoAtualizado.NomeMedico != null)
            {
                medicoBuscado.NomeMedico = medicoAtualizado.NomeMedico;
            }
            if (medicoAtualizado.Crm != null)
            {
                medicoBuscado.Crm = medicoAtualizado.Crm;
            }

            ctx.Update(medicoBuscado);

            ctx.SaveChanges();
        }

        public Medico BuscarPorId(int id)
        {
            return ctx.Medicos.FirstOrDefault(m => m.IdMedico == id);
        }

        public void Cadastrar(Medico novaMedico)
        {
            ctx.Medicos.Add(novaMedico);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            Medico medicoBuscado = ctx.Medicos.FirstOrDefault(m => m.IdMedico == id);

            ctx.Medicos.Remove(medicoBuscado);

            ctx.SaveChanges();
        }

        public List<Medico> Listar()
        {
            return ctx.Medicos.ToList();
        }

        public List<Medico> ListarConsultas(int id)
        {
            var medicoBuscado = ctx.Medicos.Include(p => p.IdUsuarioNavigation)
                                               .Where(p => p.IdUsuario == id)
                                               .Select(p => new Medico()

        {
            IdMedico = p.IdMedico,

            NomeMedico = p.NomeMedico,

            IdUsuarioNavigation = new Usuario()
            {
                IdUsuario = p.IdUsuarioNavigation.IdUsuario,

                Email = p.IdUsuarioNavigation.Email
            },

            IdEspecialidadeNavigation = new Especialidade()
            {
                IdEspecialidade = p.IdEspecialidadeNavigation.IdEspecialidade,

                NomeEspecialidade = p.IdEspecialidadeNavigation.NomeEspecialidade
            },

            IdClinicaNavigation = new Clinica()
            {
                IdClinica = p.IdClinicaNavigation.IdClinica,
                
                HorarioAbertura = p.IdClinicaNavigation.HorarioAbertura,

                HorarioFechamento = p.IdClinicaNavigation.HorarioFechamento,

                Endereco = p.IdClinicaNavigation.Endereco,

                NomeFantasia = p.IdClinicaNavigation.NomeFantasia,


                RazaoSocial = p.IdClinicaNavigation.RazaoSocial
            }
            
        });
            return medicoBuscado.ToList();
        }
    }
}