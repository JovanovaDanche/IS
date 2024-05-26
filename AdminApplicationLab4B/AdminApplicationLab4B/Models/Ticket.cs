using System.ComponentModel.DataAnnotations;
using System;

namespace AdminApplicationLab4B.Models
{
    public class Ticket
    {
        public Guid ConcertId { get; set; }
        public Concert? Concert { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Rating { get; set; }
        public virtual EShopApplicationUser? CreatedBy { get; set; }
        public virtual ICollection<TicketInShoppingCart>? ProductsInShoppingCart { get; set; }
        public ICollection<TicketInOrder>? ProductInOrders { get; set; }
    }
}
