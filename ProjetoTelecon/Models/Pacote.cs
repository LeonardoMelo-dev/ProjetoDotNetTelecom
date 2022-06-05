namespace ProjetoTelecon.Models
{
    public class Pacote
    {
        public Pacote()
        {
            Usuario = new HashSet<Usuario>();
        }
        public int PacoteId { get; set; }
        public string? Nome { get; set; }
        public int? Mensalidade { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataModificacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public int ServicoId { get; set; }
        public virtual Servico Servico { get; set; }
        public virtual IEnumerable<Usuario> Usuario { get; set; }
    }
}
