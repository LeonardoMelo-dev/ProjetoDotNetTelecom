using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTelecon.Models
{
    public class Packets_Users
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Packets_UsersId { get; set; }
        public int PacketId { get; set; }
        public int UserId { get; set; }

        public virtual Users Users { get; set; }
        public virtual Packets Packets { get; set; }
    }
}
