using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Upd8.Web.Models.Emums;

namespace Upd8.Web.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(11)]
        [DisplayName("CPF")]
        public string CPF { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [DisplayName("Sexo")]
        public SexoEnum Sexo { get; set; }

        [Required]
        [DisplayName("Endereço")]
        public string Endereco { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Cidade")]
        public string Cidade { get; set; }

        [Required]
        [StringLength(2)]
        [DisplayName("Estado")]
        public string Estado { get; set; }
    }
}
