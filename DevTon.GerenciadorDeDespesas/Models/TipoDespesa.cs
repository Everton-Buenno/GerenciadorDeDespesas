using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevTon.GerenciadorDeDespesas.Models
{
    public class TipoDespesa
    {

        
        public int TipoDespesaId { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        [Remote("TipoDespesaExist", "TipoDespesas")]
        public string Nome { get; set; }

        public IEnumerable<Despesa> Despesas { get; set; }
    }
}
