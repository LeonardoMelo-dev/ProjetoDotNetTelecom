using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTelecon.Models
{
    public class Packets_Services
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Packets_ServicesId { get; set; }
        public int PacketId { get; set; }
        public int ServiceId { get; set; }

        public virtual Services Services { get; set; }
        public virtual Packets Packets { get; set; }  
    }
}
