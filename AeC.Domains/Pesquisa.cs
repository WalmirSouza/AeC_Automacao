using AeC.Domains.Base;

namespace AeC.Domains
{
    public class Pesquisa : BaseEntity
    {
        public string Titulo { get; set; }
        public string Area { get; set; }
        public string Autor { get; set; }
        public string Descricao { get; set; }
        public string DataDePublicacao { get; set; }

    }
}
