namespace ProjetoTelecon.Models
{
    public class Users
    {
        public Users()
        {
            Packets_Users = new HashSet<Packets_Users>();
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; } 
        public string Password { get; set; }
        public string? CPF { get; set; }
        public string? Image { get; set; }
        public double? Balance { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool IsAdmin { get; set; }
        public bool Active { get; set; }
        public virtual IEnumerable<Packets_Users> Packets_Users { get; set; }
    }
}
