using senai.sp_medicals.webApi.Domains;
using System.Collections.Generic;

namespace senai.sp_medicals.webApi.Controllers
{
    interface IUSuarioRepository
    {
        List<Usuario> Listar();

        Usuario BuscarPorId(int id);

        void Cadastrar(Usuario novoUsuario);

        void AtualizarPorId(int id, Usuario usuarioAtualizado);

        Usuario BuscarPorEmailSenha (string email, string senha);
        
        void Deletar(int id);
    }
}
