namespace ProjetoTelecon.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int? Idade { get; set; } 
        public string? Senha { get; set; }
        public string? CPF { get; set; }
        public float Saldo { get; set; }
        public DateTime? DataModificacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public bool IsAdmin { get; set; }
        public int PacoteId { get; set; }
        public virtual Pacote Pacote { get; set; }
    }
}
