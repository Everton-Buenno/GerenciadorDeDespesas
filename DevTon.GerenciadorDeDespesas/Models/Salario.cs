using System.ComponentModel.DataAnnotations;

namespace DevTon.GerenciadorDeDespesas.Models
{
    public class Salario
    {


        public int SalarioId { get; set; }

        public int MesId { get; set; }
        public Meses Meses { get; set; }



        [Required(ErrorMessage = "Campo Obrigatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "Valor Invalido!")]
        public double Valor { get; set; }
    }
}
