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
    public class SituacaoRepository : ISituacaoRepository
    {
        /// <summary>
        /// Objeto contexto por onde serão chamados os metódos do EF Core
        /// </summary>
        MedicalsContext ctx = new MedicalsContext();

        public void AtualizarPorId(int id, Situacao situacaoAtualiza)
        {
            Situacao situacaoBuscada = ctx.Situacaos.Find(id);

            if (situacaoAtualiza != null)
            {
                situacaoBuscada.Situacao1 = situacaoAtualiza.Situacao1;
            }

            ctx.Update(situacaoBuscada);

            ctx.SaveChanges();
        }

        public Situacao BuscarPorId(int id)
        {
            return ctx.Situacaos.FirstOrDefault(e => e.IdSituacao == id);
        }

        public void Cadastrar(Situacao novaSituacao)
        {
            ctx.Situacaos.Add(novaSituacao);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            Situacao situacaoBuscada = ctx.Situacaos.FirstOrDefault(e => e.IdSituacao == id);

            ctx.Situacaos.Remove(situacaoBuscada);

            ctx.SaveChanges();
        }

        public List<Situacao> Listar()
        {
            return ctx.Situacaos.ToList();
        }
    }
}
