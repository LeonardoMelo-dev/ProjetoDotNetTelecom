namespace ProjetoTelecon.Models
{
    public class Packets
    {
        public Packets()
        {
            Packets_Users = new HashSet<Packets_Users>();
            Packets_Services = new HashSet<Packets_Services>();
        }

        public int PacketId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Active { get; set; }
        public string? Image { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual IEnumerable<Packets_Users> Packets_Users { get; set; }
        public virtual IEnumerable<Packets_Services> Packets_Services { get; set; }

    }
}
