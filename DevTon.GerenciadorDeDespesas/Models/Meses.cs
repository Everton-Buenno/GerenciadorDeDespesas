namespace DevTon.GerenciadorDeDespesas.Models
{
    public class Meses
    {

        public int MesId { get; set; }

        public string Nome { get; set; }


        public IEnumerable<Despesa> Despesas { get; set; }


        public Salario Salarios { get; set; }
    }
}
