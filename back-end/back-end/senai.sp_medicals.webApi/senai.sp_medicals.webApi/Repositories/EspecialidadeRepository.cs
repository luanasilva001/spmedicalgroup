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
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        /// <summary>
        /// Objeto contexto por onde serão chamados os metódos do EF Core
        /// </summary>
        MedicalsContext ctx = new MedicalsContext();

        /// <summary>
        /// Atualiza uma especialidade existente
        /// </summary>
        /// <param name="id">Id da especialidade que será atualizada</param>
        /// <param name="especialidadeAtualizada">Objeto especialidadeAtualizada com as novas informações</param>
        public void AtualizarPorId(int id, Especialidade especialidadeAtualizada)
        {
            Especialidade especialidadeBuscada = ctx.Especialidades.Find(id);

            if (especialidadeAtualizada.NomeEspecialidade != null)
            {
                especialidadeBuscada.NomeEspecialidade = especialidadeAtualizada.NomeEspecialidade;
            }

            ctx.Update(especialidadeBuscada);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Busca uma especialidade pelo seu id
        /// </summary>
        /// <param name="id">Id da especialidae que será buscada</param>
        /// <returns>Uma especialidade buscada</returns>
        public Especialidade BuscarPorId(int id)
        {
            return ctx.Especialidades.FirstOrDefault(e => e.IdEspecialidade == id);
        }

        /// <summary>
        /// Cadastra uma especialidade 
        /// </summary>
        /// <param name="novoTipoUsuario">Objeto novaEspecialidade que será cadastrada</param>
        public Especialidade Cadastrar(Especialidade novaEspecialidade)
        {
            ctx.Especialidades.Add(novaEspecialidade);

            ctx.SaveChanges();

            return novaEspecialidade;
        }

        /// <summary>
        /// Deleta uma especialidade existente
        /// </summary>
        /// <param name="id">ID da especialidade que será deletada</param>
        public void Deletar(int id)
        {
            Especialidade especialidadeBuscada = ctx.Especialidades.FirstOrDefault(e => e.IdEspecialidade == id);

            ctx.Especialidades.Remove(especialidadeBuscada);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Retorna uma lista de especialidades
        /// </summary>
        /// <returns>Uma lista de especialidades</returns>
        public List<Especialidade> Listar()
        {
            return ctx.Especialidades.ToList();
        }

        public List<Especialidade> ListarCadastro(int id)
        {
            return ctx.Especialidades.Where(e => e.IdEspecialidade == id).Select(e => new Especialidade() { IdEspecialidade = e.IdEspecialidade, NomeEspecialidade = e.NomeEspecialidade }).ToList();
        }
    }
}
