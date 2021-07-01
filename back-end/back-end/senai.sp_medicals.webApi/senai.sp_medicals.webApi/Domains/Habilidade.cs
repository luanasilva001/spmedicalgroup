using System;
using System.Collections.Generic;

#nullable disable

namespace senai.sp_medicals.webApi.Domains
{
    public partial class Habilidade
    {
        public int IdHabilidade { get; set; }
        public string NomeHabilidade { get; set; }
        public int? IdTipoHabilidade { get; set; }

        public virtual TipoHabilidade IdTipoHabilidadeNavigation { get; set; }
    }
}
