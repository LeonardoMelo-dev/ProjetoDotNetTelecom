using System.Collections.ObjectModel;

namespace ProjetoTelecon.Models
{
    public class Packets
    {
        public Packets()
        {
            Packets_Services = new Collection<Packets_Services>();
            Packets_Users = new Collection<Packets_Users>();
        }

        public int PacketId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Active { get; set; }
        public string? Image { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<Packets_Services> Packets_Services { get; set; }
        public virtual ICollection<Packets_Users> Packets_Users { get; set; }
    }
}
