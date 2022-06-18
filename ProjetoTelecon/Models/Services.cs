namespace ProjetoTelecon.Models
{
    public class Services
    {
        public Services()
        {
            Packets_Services = new HashSet<Packets_Services>();
        }

        public int ServiceId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int? Franchise { get; set; }
        public int? ExtraFranchise { get; set; }
        public string? ServiceType { get; set; }
        public bool Active { get; set; }
        public string? Image { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public virtual IEnumerable<Packets_Services> Packets_Services { get; set; }
    }
}
