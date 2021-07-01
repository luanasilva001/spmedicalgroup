using System;
using System.Collections.Generic;

#nullable disable

namespace senai.sp_medicals.webApi.Domains
{
    public partial class Personagem
    {
        public int IdPersonagem { get; set; }
        public string NomePersonagem { get; set; }
        public int CapacidadeMaximaVida { get; set; }
        public int CapacidadeMaximaMana { get; set; }
        public DateTime DataAtualização { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public int? IdClasse { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Classe IdClasseNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
