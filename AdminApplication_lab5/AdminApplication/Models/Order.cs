using System.ComponentModel.DataAnnotations;

namespace AdminApplication.Models
{
    public class Order 
    {
        public Guid Id { get; set; }
        public string? OwnerId { get; set; }
        public EShopApplicationUser? Owner { get; set; }
        public IEnumerable<TicketInOrder>? ProductInOrders { get; set; }
    }
}
