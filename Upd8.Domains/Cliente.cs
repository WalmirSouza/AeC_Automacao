using Upd8.Domains.Base;
using Upd8.Domains.Enums;

namespace Upd8.Domains
{
    public class Cliente : BaseEntity
    {
        public string CPF { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public SexoEnum Sexo { get; set; }

        public string Endereco { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

    }
}
