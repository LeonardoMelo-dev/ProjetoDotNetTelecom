namespace ProjetoTelecon.Models
{
    public class Servico
    {
        public Servico()
        {
            Pacote = new HashSet<Pacote>();
        }

        public int ServicoId { get; set; }
        public string? Nome { get; set; }
        public int Franquia { get; set; }
        public int FranquiaExtra { get; set; }
        public string? TipoServico { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataModificacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public virtual IEnumerable<Pacote> Pacote { get; set; }
    }
}
