using Microsoft.EntityFrameworkCore;
using senai.sp_medicals.webApi.Contexts;
using senai.sp_medicals.webApi.Controllers;
using senai.sp_medicals.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Classe responsável pelo repositório das especialidades
/// </summary>
namespace senai.sp_medicals.webApi.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        /// <summary>
        /// Objeto contexto por onde serão chamados os metódos do EF Core
        /// </summary>
        MedicalsContext ctx = new MedicalsContext();

        public void AtualizarPorId(int id, Paciente pacienteAtualizado)
        {
            Paciente pacienteBuscado = ctx.Pacientes.Find(id);

            if (pacienteAtualizado.DataNascimento != DateTime.Now)
            {
                pacienteBuscado.DataNascimento = pacienteAtualizado.DataNascimento;
            }
            if (pacienteAtualizado.NomePaciente != null)
            {
                pacienteBuscado.NomePaciente = pacienteAtualizado.NomePaciente;
            }
            if (pacienteAtualizado.Rg != null)
            {
                pacienteBuscado.Rg = pacienteAtualizado.Rg;
            }
            if (pacienteAtualizado.Cpf != null)
            {
                pacienteBuscado.Cpf = pacienteAtualizado.Cpf;
            }
            if (pacienteAtualizado.Telefone != null)
            {
                pacienteBuscado.Telefone = pacienteAtualizado.Telefone;
            }
            if (pacienteAtualizado.Endereco != null)
            {
                pacienteBuscado.Endereco = pacienteAtualizado.Endereco;
            }

            ctx.Update(pacienteBuscado);

            ctx.SaveChanges();
        }

        public Paciente BuscarPorId(int id)
        {
            return ctx.Pacientes.FirstOrDefault(p => p.IdPaciente == id);
        }

        public Paciente Cadastrar(Paciente novoPaciente)
        {
            ctx.Pacientes.Add(novoPaciente);

            ctx.SaveChanges();

            return novoPaciente;
        }

        public void Deletar(int id)
        {
            Paciente pacienteBuscado = ctx.Pacientes.FirstOrDefault(p => p.IdPaciente == id);

            ctx.Pacientes.Remove(pacienteBuscado);

            ctx.SaveChanges();
        }

        public List<Paciente> Listar()
        {
            return ctx.Pacientes.ToList();
        }

        public List<Paciente> ListarConsultas(int id)
        {
            var pacienteBuscado = ctx.Pacientes.Include(p => p.IdUsuarioNavigation)
                                               .Where(p => p.IdUsuario == id)
                                               .Select(p => new Paciente()
            {
                IdPaciente = p.IdPaciente,
                NomePaciente = p.NomePaciente,
                DataNascimento = p.DataNascimento,
                Rg = p.Rg,
                Cpf = p.Cpf,
                Telefone = p.Telefone,
                Endereco = p.Endereco
            });

            return pacienteBuscado.ToList();
        }
    }
}