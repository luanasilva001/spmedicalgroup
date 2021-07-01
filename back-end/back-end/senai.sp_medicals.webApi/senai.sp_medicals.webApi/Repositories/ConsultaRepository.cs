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
  public class ConsultaRepository : IConsultaRepository
  {
    /// <summary>
    /// Objeto contexto por onde serão chamados os metódos do EF Core
    /// </summary>
    MedicalsContext ctx = new MedicalsContext();

    public void AtualizarPorId(int id, Consultum consultaAtualiza)
    {
      Consultum consultaBuscada = ctx.Consulta.Find(id);

      if (consultaAtualiza.IdMedico != null)
      {
        consultaBuscada.IdMedico = consultaAtualiza.IdMedico;
      }
      if (consultaAtualiza.IdPaciente != null)
      {
        consultaBuscada.IdPaciente = consultaAtualiza.IdPaciente;
      }
      if (consultaAtualiza.IdSituacao != null)
      {
        consultaBuscada.IdSituacao = consultaAtualiza.IdSituacao;
      }
      if (consultaAtualiza.Descricao != null)
      {
        consultaBuscada.Descricao = consultaAtualiza.Descricao;
      }

      ctx.Update(consultaBuscada);

      ctx.SaveChanges();
    }

    /// <summary>
    /// Muda a descrição de uma consulta
    /// </summary>
    /// <param name="id">Id da consulta com a descrição mudada</param>
    /// <param name="status">Status da nova descrição</param>
    public void MudarDescricao(int id, Consultum status)
    {
      Consultum consultaBuscada = ctx.Consulta.Find(id);

      if (status.Descricao != null)
      {
        consultaBuscada.Descricao = status.Descricao;
      }

      ctx.Update(consultaBuscada);

      ctx.SaveChanges();
    }

    /// <summary>
    /// Agenda, realiza ou cancela uma situação
    /// </summary>
    /// <param name="id">Id da consulta</param>
    /// <param name="status">status da situação que será mudada</param>
    public void AprovarRecusar(int id, string status)
    {
      Consultum consultaBuscada = ctx.Consulta
                                    .FirstOrDefault(p => p.IdConsulta == id);

      switch (status)
      {
        case "1":
          consultaBuscada.IdSituacao = 1;
          break;

        case "2":
          consultaBuscada.IdSituacao = 2;
          break;

        case "3":
          consultaBuscada.IdSituacao = 3;
          break;

        default:
          consultaBuscada.IdSituacao = consultaBuscada.IdSituacao;
          break;
      }

      ctx.Consulta.Update(consultaBuscada);

      ctx.SaveChanges();
    }

    public Consultum BuscarPorId(int id)
    {
      return ctx.Consulta.FirstOrDefault(u => u.IdConsulta == id);
    }

    public void Cadastrar(Consultum novaConsulta)
    {
      ctx.Consulta.Add(novaConsulta);

      ctx.SaveChanges();
    }

    public void Deletar(int id)
    {
      Consultum consultaBuscada = ctx.Consulta.FirstOrDefault(u => u.IdConsulta == id);

      ctx.Consulta.Remove(consultaBuscada);

      ctx.SaveChanges();
    }

    public List<Consultum> ListarTudo()
    {
      return ctx.Consulta.Include(c => c.IdMedicoNavigation)
                         .Include(c => c.IdPacienteNavigation)
                         .Include(c => c.IdSituacaoNavigation)
                         .Select(c => new Consultum()
                         {
                           IdConsulta = c.IdConsulta,
                           Descricao = c.Descricao,
                           DataConsulta = c.DataConsulta,

                           IdMedicoNavigation = new Medico()
                           {
                             IdMedico = c.IdMedicoNavigation.IdMedico,
                             NomeMedico = c.IdMedicoNavigation.NomeMedico,
                             IdEspecialidade = c.IdMedicoNavigation.IdEspecialidade,
                             IdClinica = c.IdMedicoNavigation.IdClinica,


                             IdEspecialidadeNavigation = new Especialidade()
                             {
                               IdEspecialidade = c.IdMedicoNavigation.IdEspecialidadeNavigation.IdEspecialidade,
                               NomeEspecialidade = c.IdMedicoNavigation.IdEspecialidadeNavigation.NomeEspecialidade
                             }

                           },

                           IdPacienteNavigation = new Paciente()
                           {
                             IdPaciente = c.IdPacienteNavigation.IdPaciente,
                             NomePaciente = c.IdPacienteNavigation.NomePaciente,
                             DataNascimento = c.IdPacienteNavigation.DataNascimento,
                             Rg = c.IdPacienteNavigation.Rg,
                             Cpf = c.IdPacienteNavigation.Cpf,
                             Endereco = c.IdPacienteNavigation.Endereco
                           },

                           IdSituacaoNavigation = new Situacao()
                           {
                             IdSituacao = c.IdSituacaoNavigation.IdSituacao,
                             Situacao1 = c.IdSituacaoNavigation.Situacao1
                           }

                         }).ToList();
    }

    public List<Consultum> ListarConsultasPaciente(int id)
    {
      return ctx.Consulta.Include(c => c.IdMedicoNavigation)
                       .Include(c => c.IdPacienteNavigation)
                       .Include(c => c.IdSituacaoNavigation)
                       .Where(c => c.IdPaciente == id)

                       .Select(c => new Consultum()
                       {
                         IdConsulta = c.IdConsulta,
                         Descricao = c.Descricao,
                         DataConsulta = c.DataConsulta,

                         IdMedicoNavigation = new Medico()
                         {
                           IdMedico = c.IdMedicoNavigation.IdMedico,
                           NomeMedico = c.IdMedicoNavigation.NomeMedico,
                           IdEspecialidade = c.IdMedicoNavigation.IdEspecialidade,
                           IdClinica = c.IdMedicoNavigation.IdClinica,


                           IdEspecialidadeNavigation = new Especialidade()
                           {
                             IdEspecialidade = c.IdMedicoNavigation.IdEspecialidadeNavigation.IdEspecialidade,
                             NomeEspecialidade = c.IdMedicoNavigation.IdEspecialidadeNavigation.NomeEspecialidade
                           }

                         },

                         IdPacienteNavigation = new Paciente()
                         {
                           IdPaciente = c.IdPacienteNavigation.IdPaciente,
                           NomePaciente = c.IdPacienteNavigation.NomePaciente,
                           DataNascimento = c.IdPacienteNavigation.DataNascimento,
                           Rg = c.IdPacienteNavigation.Rg,
                           Cpf = c.IdPacienteNavigation.Cpf,
                           Endereco = c.IdPacienteNavigation.Endereco
                         },

                         IdSituacaoNavigation = new Situacao()
                         {
                           IdSituacao = c.IdSituacaoNavigation.IdSituacao,
                           Situacao1 = c.IdSituacaoNavigation.Situacao1
                         }

                       }).ToList();
    }

    public int BuscarIdPaciente(int id)
    {
      var paciente = ctx.Pacientes.FirstOrDefault(c => c.IdUsuario == id);

      return paciente.IdPaciente;
    }

    public List<Consultum> ListarConsultasMedico(int id)
    {
      return ctx.Consulta.Include(c => c.IdMedicoNavigation)
                      .Include(c => c.IdPacienteNavigation)
                      .Include(c => c.IdSituacaoNavigation)
                      .Where(c => c.IdMedico == id)

                      .Select(c => new Consultum()
                      {
                        IdConsulta = c.IdConsulta,
                        Descricao = c.Descricao,
                        DataConsulta = c.DataConsulta,

                        IdMedicoNavigation = new Medico()
                        {
                          IdMedico = c.IdMedicoNavigation.IdMedico,
                          NomeMedico = c.IdMedicoNavigation.NomeMedico,
                          IdEspecialidade = c.IdMedicoNavigation.IdEspecialidade,
                          IdClinica = c.IdMedicoNavigation.IdClinica,


                          IdEspecialidadeNavigation = new Especialidade()
                          {
                            IdEspecialidade = c.IdMedicoNavigation.IdEspecialidadeNavigation.IdEspecialidade,
                            NomeEspecialidade = c.IdMedicoNavigation.IdEspecialidadeNavigation.NomeEspecialidade
                          }

                        },

                        IdPacienteNavigation = new Paciente()
                        {
                          IdPaciente = c.IdPacienteNavigation.IdPaciente,
                          NomePaciente = c.IdPacienteNavigation.NomePaciente,
                          DataNascimento = c.IdPacienteNavigation.DataNascimento,
                          Rg = c.IdPacienteNavigation.Rg,
                          Cpf = c.IdPacienteNavigation.Cpf,
                          Endereco = c.IdPacienteNavigation.Endereco
                        },

                        IdSituacaoNavigation = new Situacao()
                        {
                          IdSituacao = c.IdSituacaoNavigation.IdSituacao,
                          Situacao1 = c.IdSituacaoNavigation.Situacao1
                        }

                      }).ToList();
    }

    public int BuscarIdMedico(int id)
    {
      var medico = ctx.Medicos.FirstOrDefault(c => c.IdUsuario == id);

      return medico.IdMedico;
    }
  }
}