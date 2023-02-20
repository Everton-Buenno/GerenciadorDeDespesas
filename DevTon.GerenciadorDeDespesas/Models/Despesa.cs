using System.ComponentModel.DataAnnotations;

namespace DevTon.GerenciadorDeDespesas.Models
{
    public class Despesa
    {


        public int DespesaId { get; set; }

        public int MesId { get; set; }

        public Meses Meses { get; set; }


        public int TipoDespesaId { get; set; }
        public TipoDespesa TipoDespesa { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "Valor Invalido!")]
        public double Valor { get; set; }
    }
}
